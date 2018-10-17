using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaInfo;

namespace Winmedia_Database_Client
{
    class AudioInfo
    {
        public static void Info()
        {
            /// Get Media Info
            MediaInfo.MediaInfo MI = new MediaInfo.MediaInfo();
            MI.Open("./test.flac");
            int duration = Convert.ToInt32(MI.Get(StreamKind.Audio, 0, "Duration")) / 1000;
            int fileLength = Convert.ToInt32(MI.Get(0, 0, "FileSize"));
            int second = duration % 60;
            int minute = duration / 60;
            Debug.WriteLine("File length: " + fileLength);
            Debug.WriteLine("Duration: " + minute + " min " + second + " s");
        }
        
            
    }
}
