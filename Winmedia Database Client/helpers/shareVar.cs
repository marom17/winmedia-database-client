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

        public static int IdPlaylist { get => idPlaylist; set => idPlaylist = value; }
    }
}
