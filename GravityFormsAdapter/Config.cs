using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GravityFormsAdapter
{
    [DataContract]
    public class Config
    {
        public static string GetEXEFolder()
        {
            var fullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            return System.IO.Path.GetDirectoryName(fullPath);
        }
        public static string GetDownloadFolder()
        {
            var folder = Config.GetEXEFolder();
            var downloadFolder = System.IO.Path.Combine(folder, "Download");

            if (!System.IO.Directory.Exists(downloadFolder))
                System.IO.Directory.CreateDirectory(downloadFolder);

            return downloadFolder;
        }

        [DataMember]
        public string RootURL { get; set; } = "https://example.com/wp-json/gf/v2/";
        public enum APITypes
        {
            Version1,
            Version2Basic,
            Version2OAuth1,
            Version2OAuth2c
        }
        [DataMember]
        public APITypes APIType { get; set; } = APITypes.Version2OAuth1;
        [DataMember]
        public string ConsumerKey { get; set; } = "ck_12345";
        [DataMember]
        public string ConsumerSecret { get; set; } = "cs_56789";
        [DataMember]
        public bool IgnoreCertificateErrors { get; set; } = false;
        [DataMember]
        public bool FetchForms { get; set; } = true;
        [DataMember]
        public bool SaveToSQL { get; set; } = false;
        
        [DataMember]
        // Next entry ID to fetch
        public int CurrentEntryID { get; set; } = 1;
        [DataMember]
        // If this is enabled the app will start, download, then exit
        public bool AutomaticMode { get; set; } = false;
        [DataMember]
        // Comma seperated form IDs to download
        public string FormIDs { get; set; } = "";
        [DataMember]
        // If this is enabled the app will write logs to an SQL table tblGravityFormLogs
        public bool WriteSQLLogs { get; set; } = false;
        public void Save()
        {
            var folder = GetEXEFolder();
            var thisJson = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "Config.json"), thisJson);
        }
        public static Config Load()
        {
            var folder = GetEXEFolder();
            var fullPath = System.IO.Path.Combine(folder, "Config.json");
            if( System.IO.File.Exists(fullPath) )
            {
                var thisJson = System.IO.File.ReadAllText(fullPath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(thisJson);
            }
            else
            {
                return new Config();
            }
            
        }
    }
}
