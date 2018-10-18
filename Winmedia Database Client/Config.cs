using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static String _DBPort;
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

        public static String DBPort
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

                    _VlcPath = items["VlcPath"];
                    _DBHost = items["DBHost"];
                    _DBPort = items["DBPort"];
                    _DBUser = items["DBUser"];
                    _DBPass = items["DBPass"];
                    _DB = items["DB"];
                    
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

                ConfigWindow test = new ConfigWindow();
                test.Show();

                String json = JsonConvert.SerializeObject(config);
                File.WriteAllText(_confPath, json);
            }
        }

        public static void saveConfig(ConfigWindow configWindow)
        {
            _VlcPath = configWindow.VLCPath.Text;
            _DBHost = configWindow.DBHost.Text;
            _DBPort = configWindow.DBPort.Text;
            _DBUser = configWindow.DBUser.Text;
            _DBPass = configWindow.DBPass.Password;
            _DB = configWindow.DBTable.Text;

            Dictionary<String, String> config = new Dictionary<string, string>();
            config.Add("VlcPath", _VlcPath);
            config.Add("DBHost", _DBHost);
            config.Add("DBPort", _DBPort);
            config.Add("DBUser", _DBUser);
            config.Add("DBPass", _DBPass);
            config.Add("DB", _DB);

            String json = JsonConvert.SerializeObject(config);
            File.WriteAllText(_confPath, json);
        }
    }
}
