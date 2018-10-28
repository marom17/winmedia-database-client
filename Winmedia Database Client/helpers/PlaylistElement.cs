using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{
    public class PlaylistElement
    {
        private Music music;
        private TimeSpan _start = new TimeSpan(0, 0, 0);
        public TimeSpan Start { get {
                return _start;
            }
            set
            {
                _start = value;
            }
        }
        public TimeSpan PrettyTime { get => music.PrettyTime; }
        public String Performer { get => music.Artist; }
        public String Title { get => music.Title; }
        public Music Music { get => music; }

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
