using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaInfo;

namespace Winmedia_Database_Client
{
    class AudioInfo
    {
        public static Dictionary<String,String> Info(String uri)
        {
            Dictionary<String, String> info = new Dictionary<String, String>();

            String fileName = Path.GetFileName(uri);
            /// Get Media Info
            MediaInfo.MediaInfo MI = new MediaInfo.MediaInfo();
            MI.Open(uri);
            String artist = MI.Get(StreamKind.General, 0, "Performer");
            String trackName = MI.Get(StreamKind.General, 0, "Title");           
            String duration = MI.Get(StreamKind.Audio, 0, "Duration");
            String fileLength = MI.Get(0, 0, "FileSize");
            String bitrate = MI.Get(StreamKind.Audio, 0, "BitRate");

            info.Add("FileName", fileName);
            info.Add("Artist", artist);
            info.Add("Title", trackName);
            info.Add("Duration", duration);
            info.Add("FileLength", fileLength);
            info.Add("Bitrate", bitrate);

            MI.Close();

            return info;
        }
        
            
    }
}
