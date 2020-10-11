using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
        private async Task<string> GetJSON(string apiPath)
        {
            var requestUrl = config.RootURL + apiPath;

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

            
            if( true ) {
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

                // The below didn't work on the first try but the above does, so stick with the above..
                // Alternatively: 
                //req.Headers.Remove("Authorization");
                //req.Headers.Add("Authorization", c.GetAuthorizationHeader());
            }
            else
            {
                req.RequestUri = new Uri(requestUrl);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", config.ConsumerKey + ":" + config.ConsumerSecret);
            }

            //try
            {

                Log("Downloading " + apiPath);
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

            if (config.FetchForms)
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = 0;
                lblStatus.Content = "Filtering allowed form IDs";
                var validForms = config.FormIDs.Split(',').Where(f => f.Length > 0).Select(f => f.Trim()).ToList();


                progressBar.Value = 10;
                lblStatus.Content = "Fetching form headers";
                var formsJson = await GetJSON("forms");

                progressBar.Value = 20;
                lblStatus.Content = "Parsing form headers";
                var formHeaders = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, GravityDataStructures.GravityForm>>(formsJson);

                progressBar.Value = 25;
                lblStatus.Content = "Fetching form details";
                foreach (var f in formHeaders.Where(frm => validForms.Count == 0 || validForms.Contains(frm.Key)))
                {
                    var formDetailJson = await GetJSON("forms/" + f.Value.id);

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
                    var entryJSON = await GetJSON("entries/" + config.CurrentEntryID.ToString());
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

