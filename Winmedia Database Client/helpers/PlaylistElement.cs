using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    public class PlaylistElement
    {
        private Music music;

        public PlaylistElement(Music music)
        {
            this.music = music;
        }

        public override string ToString()
        {
            return (music.Artist + "/" + music.Title);
        }
    }
}
