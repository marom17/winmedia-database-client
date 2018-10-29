using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class ShareVar
    {
        private static int idPlaylist = 0;
        private static DateTime day = DateTime.Today;
        private static String hour = "00h";

        public static int IdPlaylist { get => idPlaylist; set => idPlaylist = value; }
        public static DateTime Day { get => day; set => day = value; }
        public static string Hour { get => hour; set => hour = value; }
    }
}
