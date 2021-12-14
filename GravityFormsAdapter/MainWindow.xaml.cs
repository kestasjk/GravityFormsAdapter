using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GravityFormsAdapter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        internal List<GravityDataStructures.GravityForm> Forms = new List<GravityDataStructures.GravityForm>();
        internal List<GravityDataStructures.GravityEntry> Entries = new List<GravityDataStructures.GravityEntry>();

        private static Config config = new Config();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (config.AutomaticMode)
                    MessageBox.Show("Cannot run manually when in automatic mode. Disable automatic mode first.");
                else
                    await Run();
            }
            catch(Exception ex)
            {
                int a = 0;
                a++;
                throw ex;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) => SaveConfig();

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadConfig();

        private void LoadConfig()
        {
            config = Config.Load();
            txtURL.Text = config.RootURL;
            txtKey.Text = config.ConsumerKey;
            txtSecret.Text = config.ConsumerSecret;
            txtFormIDs.Text = config.FormIDs;
            txtCurrentEntryID.Text = config.CurrentEntryID.ToString();
            chkAutoRun.IsChecked = (config.AutomaticMode);
            chkIgnoreSSL.IsChecked = (config.IgnoreCertificateErrors);
            chkFetchForms.IsChecked = (config.FetchForms);
            chkSaveToSQL.IsChecked = (config.SaveToSQL);
            cboOptionVersion1.IsSelected = config.APIType == Config.APITypes.Version1;
            cboOptionVersion2Basic.IsSelected = config.APIType == Config.APITypes.Version2Basic;
            cboOptionVersion2OAuth1.IsSelected = config.APIType == Config.APITypes.Version2OAuth1;
        }
        private void SaveConfig()
        {
            int currentEntryID = -1;
            if (Int32.TryParse(txtCurrentEntryID.Text, out currentEntryID))
            {
                config.CurrentEntryID = currentEntryID;
            }
            config.RootURL = txtURL.Text;
            config.ConsumerKey = txtKey.Text;
            config.ConsumerSecret = txtSecret.Text;
            config.FormIDs = txtFormIDs.Text;
            config.AutomaticMode = chkAutoRun.IsChecked ?? false;
            config.IgnoreCertificateErrors = chkIgnoreSSL.IsChecked ?? false;
            config.SaveToSQL = chkSaveToSQL.IsChecked ?? false;
            config.FetchForms = chkFetchForms.IsChecked ?? false;
            if (cboOptionVersion1.IsSelected)
                config.APIType = Config.APITypes.Version1;
            else if (cboOptionVersion2Basic.IsSelected)
                config.APIType = Config.APITypes.Version2Basic;
            else
                config.APIType = Config.APITypes.Version2OAuth1;

            config.Save();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig();

            if (config.AutomaticMode)
            {
                await Task.Delay(5000);

                await Run();

                await Task.Delay(5000);

                Close();
            }
            else
            {
                btnRun.IsEnabled = true;
            }
        }
        public class PageNotFoundException : Exception
        {
            public PageNotFoundException() : base("404 error, page not found") { }
        }

        public class GravityFormsV1Signer
        {

            public static string UrlEncodeTo64(byte[] bytesToEncode)
            {
                string returnValue
                    = System.Convert.ToBase64String(bytesToEncode);

                return HttpUtility.UrlEncode(returnValue);
            }

            public static string Sign(string value, string key)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(key)))
                {
                    return UrlEncodeTo64(hmac.ComputeHash(Encoding.ASCII.GetBytes(value)));
                }
            }

            public static int UtcTimestamp(TimeSpan timeSpanToAdd)
            {
                TimeSpan ts = (DateTime.UtcNow.Add(timeSpanToAdd) - new DateTime(1970, 1, 1, 0, 0, 0));
                int expires_int = (int)ts.TotalSeconds;
                return expires_int;
            }
        }
        private async Task<string> GetJSON(string route, Config.APITypes apiType)
        {
            var apiRoot = "";
            switch(apiType)
            {
                case Config.APITypes.Version1: apiRoot = "gravityformsapi/"; break;
                case Config.APITypes.Version2Basic: apiRoot = ""; route = "wp-json/gf/v2/" + route; break;
                case Config.APITypes.Version2OAuth1: apiRoot = ""; route = "wp-json/gf/v2/" + route; break;
                case Config.APITypes.Version2OAuth2c: apiRoot = ""; route = "wp-json/gf/v2/" + route; break;
            }
            var requestUrl = config.RootURL + apiRoot + route;

            Log("Preparing web request");

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (config.IgnoreCertificateErrors) ServicePointManager.ServerCertificateValidationCallback += (sn, cert, chain, sslPolicyErrors) => true;

            var req = new HttpRequestMessage();
            req.Method = HttpMethod.Get;
            if (false)
            {
                req.Method = HttpMethod.Put;
                req.Content = new StringContent("{ 'is_read':1 }");
                req.Content.Headers.Remove("Content-Type");
                req.Content.Headers.Add("Content-Type", "application/json");

            }


            switch (apiType)
            {
                case Config.APITypes.Version1:
                    Log("Preparing v1 credentials:");
                    string publicKey = config.ConsumerKey;
                    string privateKey = config.ConsumerSecret;
                    string method = "GET";
                    string expires = GravityFormsV1Signer.UtcTimestamp(new TimeSpan(0, 1, 0)).ToString();
                    string stringToSign = string.Format("{0}:{1}:{2}:{3}", publicKey, method, route, expires);


                    var sig = GravityFormsV1Signer.Sign(stringToSign, privateKey);
                    Log("Signature:" + sig);

                    var signedURL = requestUrl + "?api_key=" + publicKey + "&signature=" + sig + "&expires=" + expires;

                    Log("URL:" + signedURL);

                    req.RequestUri = new Uri(signedURL);
                    break;
                case Config.APITypes.Version2Basic:
                    Log("Preparing Basic credentials");

                    // The below didn't work on the first try but the below does, so stick with the below..
                    req.Headers.Remove("Authorization");
                    //var authHeader = c.GetAuthorizationHeader();
                    var authBasic = Convert.ToBase64String(Encoding.UTF8.GetBytes(config.ConsumerKey + ":" + config.ConsumerSecret));

                    req.Headers.Add("Authorization", "Basic " + authBasic);

                    req.RequestUri = new Uri(requestUrl);
                    break;

                case Config.APITypes.Version2OAuth1:
                    Log("Preparing OAuth credentials");

                    OAuth.OAuthRequest c = OAuth.OAuthRequest.ForRequestToken(config.ConsumerKey, config.ConsumerSecret);
                    c.RequestUrl = requestUrl;
                    var oAuthParams = c.GetAuthorizationQuery();

                    Log("OAuth parameters:");
                    Log(oAuthParams);

                    Log("Final URL:");
                    var authenticatedURL = requestUrl + "?" + oAuthParams;
                    Log(authenticatedURL);

                    req.RequestUri = new Uri(authenticatedURL);
                    break;
            }

            //try
            {

                Log("Downloading " + route);
                var result = await httpClient.SendAsync(req);

                if (!result.IsSuccessStatusCode)
                {
                    if( result.StatusCode == HttpStatusCode.NotFound )
                    {
                        // This is okay, it is likely just an entry that isn't there yet
                        throw new PageNotFoundException();
                    }
                    throw new Exception("HTTP error: " + result.StatusCode.ToString() + " : " + result.ReasonPhrase + "\n" + result.ToString());
                }

                var jsonData = await result.Content.ReadAsStringAsync();

                if( apiType == Config.APITypes.Version1)
                {
                    // For V1 compatibility remove the envelope:
                    jsonData = System.Text.RegularExpressions.Regex.Replace(jsonData, "^\\{\"status\":200,\"response\":", "");
                    jsonData = System.Text.RegularExpressions.Regex.Replace(jsonData, "\\}$", "");
                }

                return jsonData;
                Log("Downloaded");
                Log(jsonData);
            }
            /*catch (Exception ex)
            {
                Log("Exception occurred:");
                Log(ex.Message);
                Log(ex.StackTrace);
            }*/

            Log("Done");
            
            return null;
        }
        private void Log(string line)
        {
            lblResults.Content = line;
            System.IO.File.AppendAllText(System.IO.Path.Combine(Config.GetDownloadFolder(), "log.txt"), line+ "\n");
        }

        private async Task Run()
        {
            if( config.CurrentEntryID < 1 )
            {
                throw new Exception("The current entry ID is less than 1, but it starts at 1");
            }
            dgdForms.ItemsSource = null;
            var formDetails = new Dictionary<string, GravityDataStructures.GravityForm>();

            var apiType = config.APIType;
            if (config.FetchForms)
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = 0;
                lblStatus.Content = "Filtering allowed form IDs";
                var validForms = config.FormIDs.Split(',').Where(f => f.Length > 0).Select(f => f.Trim()).ToList();

                progressBar.Value = 10;
                lblStatus.Content = "Fetching form headers";
                var formsJson = await GetJSON("forms", apiType);

                progressBar.Value = 20;
                lblStatus.Content = "Parsing form headers";
                var formHeaders = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,GravityDataStructures.GravityForm>>(formsJson);
                //var formHeaders= envelope.response;
                
                progressBar.Value = 25;
                lblStatus.Content = "Fetching form details";
                foreach (var f in formHeaders.Where(frm => validForms.Count == 0 || validForms.Contains(frm.Key)))
                {
                    var formDetailJson = await GetJSON("forms/" + f.Value.id, apiType);

                    lblStatus.Content = "Fetching form " + f.Value.id;
                    var formDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<GravityDataStructures.GravityForm>(formDetailJson);

                    formDetails.Add(f.Key, formDetail);

                    dgdForms.ItemsSource = null;
                    dgdForms.ItemsSource = formDetails.Values.ToList();

                    progressBar.Value = (progressBar.Value + 5) % 100;
                }
            }


            lblStatus.Content = "Fetching entries, from " + config.CurrentEntryID;

            var newEntries = new List<GravityDataStructures.GravityEntry>();
            dgdEntries.ItemsSource = null;
            dgdEntries.ItemsSource = newEntries;
            var done = false;
            do
            {
                dgdEntries.ItemsSource = null;
                dgdEntries.ItemsSource = newEntries;

                progressBar.Value = (progressBar.Value + 5) % 100;

                try
                {
                    var entryJSON = await GetJSON("entries/" + config.CurrentEntryID.ToString(), apiType);
                    var entryDictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(entryJSON);
                    if( entryDictionary.ContainsKey("form_id" ) )
                    {
                        var form_id = entryDictionary["form_id"]?.ToString() ?? "";

                        if ( formDetails.ContainsKey(form_id) )
                        {
                            newEntries.Add(new GravityDataStructures.GravityEntry(formDetails[form_id], entryDictionary));
                        }
                    }

                    config.CurrentEntryID++;

                    lblStatus.Content = "Fetching entries, from " + config.CurrentEntryID;
                }
                catch(PageNotFoundException ex)
                {
                    lblStatus.Content = " Entry not found at " + config.CurrentEntryID + ". Finished fetching.";
                    done = true;
                }
                catch (Exception ex)
                {
                    Log("Error retrieving new entry: " + ex.Message);
                    lblStatus.Content = "Error fetching: " + ex.Message;
                    done = true;
                }
                
            }
            while (!done);
            dgdEntries.ItemsSource = null;
            dgdEntries.ItemsSource = newEntries;

            progressBar.Value = 100;

            lblStatus.Content = "Fetch complete at " + config.CurrentEntryID + ". Fetched " + newEntries.Count + " new entries.";

            SaveEntriesToJSON(newEntries);

            if( config.FetchForms ) SaveFormsToJSON(formDetails.Values.ToList());

            if (config.SaveToSQL)
            {
                SaveEntriesToSQL(newEntries);

                if (config.FetchForms) SaveFormsToSQL(formDetails.Values.ToList());
            }

            // Save directly, then reload the config to the screen to refresh the current entry ID
            config.Save();

            LoadConfig();
        }

        private void SaveEntriesToJSON(List<GravityDataStructures.GravityEntry> entries)
        {

            if (entries.Count > 0)
            {
                lblStatus.Content = "Saving " + entries.Count + " new records to JSON.";
                progressBar.Value = 0;
                var downloadFolder = Config.GetDownloadFolder();
                for (int i = 0; i < entries.Count; i++)
                {
                    var fullOutputPath = System.IO.Path.Combine(downloadFolder, "FormID-" + entries[i].form_id + "_EntryID-" + entries[i].id + ".json");
                    System.IO.File.WriteAllText(fullOutputPath, Newtonsoft.Json.JsonConvert.SerializeObject(entries[i]));
                    progressBar.Value = Math.Floor((((double)i) / ((double)entries.Count) * 100.0));
                }

                progressBar.Value = 100;
                lblStatus.Content = entries.Count + " new entries written out to folder.";
            }
            else
            {
                lblStatus.Content = "No new entries to fetch.";
            }

        }

        private void SaveEntriesToSQL(List<GravityDataStructures.GravityEntry> entries)
        {

            if (entries.Count > 0)
            {
                lblStatus.Content = "Saving " + entries.Count + " new records to SQL.";
                progressBar.Value = 0;
                for (int i = 0; i < entries.Count; i++)
                {
                    SQLServer.SaveEntries(entries[i]);
                    progressBar.Value = Math.Floor((((double)i) / ((double)entries.Count) * 100.0));
                }

                progressBar.Value = 100;
                lblStatus.Content = entries.Count + " new entries written out to SQL.";
            }
            else
            {
                lblStatus.Content = "No new entries to fetch.";
            }
        }

        private void SaveFormsToJSON(List<GravityDataStructures.GravityForm> forms)
        {

            if (forms.Count > 0)
            {
                lblStatus.Content = "Saving " + forms.Count + " new records to JSON.";
                progressBar.Value = 0;
                var downloadFolder = Config.GetDownloadFolder();
                for (int i = 0; i < forms.Count; i++)
                {
                    var fullOutputPath = System.IO.Path.Combine(downloadFolder, "FormID-" + forms[i].id + ".json");
                    System.IO.File.WriteAllText(fullOutputPath, Newtonsoft.Json.JsonConvert.SerializeObject(forms[i]));
                    progressBar.Value = Math.Floor((((double)i) / ((double)forms.Count) * 100.0));
                }

                progressBar.Value = 100;
                lblStatus.Content = forms.Count + " new entries written out to folder.";
            }
            else
            {
                lblStatus.Content = "No new entries to fetch.";
            }

        }

        private void SaveFormsToSQL(List<GravityDataStructures.GravityForm> forms)
        {

            if (forms.Count > 0)
            {
                lblStatus.Content = "Saving " + forms.Count + " new records to SQL.";
                progressBar.Value = 0;
                for (int i = 0; i < forms.Count; i++)
                {
                    SQLServer.SaveForms(forms[i]);
                    progressBar.Value = Math.Floor((((double)i) / ((double)forms.Count) * 100.0));
                }

                progressBar.Value = 100;
                lblStatus.Content = forms.Count + " new entries written out to SQL.";
            }
            else
            {
                lblStatus.Content = "No new entries to fetch.";
            }
        }
        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var downloadFolder = Config.GetDownloadFolder();
            System.Diagnostics.Process.Start("explorer.exe", downloadFolder);
        }
    }
}

