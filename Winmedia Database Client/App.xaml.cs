using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MediaInfo;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            String filepath = "./config";
            String newConfig = String.Empty;
            try
            {
                using (StreamReader r = new StreamReader(filepath))
                {
                    var config = r.ReadToEnd();
                    Dictionary<String, String> items = JsonConvert.DeserializeObject<Dictionary<String, String>>(config);
                    r.Close();
                    if (!items.ContainsKey("VlcPath"))
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "VLC|vlc.exe";
                        if (openFileDialog.ShowDialog() == true)
                        {
                            items.Add("VlcPath", openFileDialog.FileName);
                            String json = JsonConvert.SerializeObject(items);
                            File.WriteAllText(filepath, json);

                        }
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Dictionary<String, String> config = new Dictionary<string, string>();
                String json = JsonConvert.SerializeObject(config);
                File.WriteAllText(filepath, json);
            }

            /// Get Media Info
            MediaInfo.MediaInfo MI = new MediaInfo.MediaInfo();
            MI.Open("./test.flac");
            int duration = Convert.ToInt32(MI.Get(StreamKind.Audio, 0, "Duration"))/1000;
            int fileLength = Convert.ToInt32(MI.Get(0, 0, "FileSize"));
            int second = duration % 60;
            int minute = duration / 60;
            Debug.WriteLine("File length: " + fileLength);
            Debug.WriteLine("Duration: " + minute + " min " + second + " s");
            
        }
    }
}
