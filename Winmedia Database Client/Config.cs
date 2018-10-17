using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class Config
    {
        private static String _confPath = "./config";

        private static String _VlcPath;
        private static String _DBHost;
        private static Int32 _DBPort;
        private static String _DBUser;
        private static String _DBPass;
        private static String _DB;
        private static String[] _format = { ".mp3",".flac",".mp2",".wav"};

        public static String VlcPath
        {
            get { return _VlcPath; }
            set { _VlcPath = value; }
        }

        public static String DBHost
        {
            get { return _DBHost; }
            set { _DBHost = value; }
        }

        public static Int32 DBPort
        {
            get { return _DBPort; }
            set { _DBPort = value; }
        }

        public static String DBUser
        {
            get { return _DBUser; }
            set { _DBUser = value; }
        }

        public static String DBPass
        {
            get { return _DBPass; }
            set { _DBPass = value; }
        }

        public static String DB
        {
            get { return _DB; }
            set { _DB = value; }
        }

        public static String[] Format
        {
            get { return _format; }
        }

        public static void Init()
        {
            String newConfig = String.Empty;
            StreamReader r = null;
            try
            {
                using (r = new StreamReader(_confPath))
                {
                    var config = r.ReadToEnd();
                    Dictionary<String, String> items = JsonConvert.DeserializeObject<Dictionary<String, String>>(config);
                    r.Close();
                    if (items["VlcPath"] == "")
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "VLC|vlc.exe";
                        if (openFileDialog.ShowDialog() == true)
                        {
                            items["VlcPath"] = openFileDialog.FileName;
                            String json = JsonConvert.SerializeObject(items);
                            File.WriteAllText(_confPath, json);

                        }
                    }
                    else
                    {
                        _VlcPath = items["VlcPath"];
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Dictionary<String, String> config = new Dictionary<string, string>();
                config.Add("VlcPath", "");
                config.Add("DBHost", "");
                config.Add("DBPort", "");
                config.Add("DBUser", "");
                config.Add("DBPass", "");
                config.Add("DB", "");

                String json = JsonConvert.SerializeObject(config);
                File.WriteAllText(_confPath, json);
            }
        }
    }
}
