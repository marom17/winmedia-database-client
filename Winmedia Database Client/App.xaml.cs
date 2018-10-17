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
            Config.Init();

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
