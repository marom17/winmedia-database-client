using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class Music
    {
        private String _filePath;
        private String _artist;
        private String _name;
        private int _duration;
        private int _intro;
        private int _next;
        private int _trimin;
        private int _trimout;

        public string FilePath { get => _filePath; set => _filePath = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public int Intro { get => _intro; set => _intro = value; }
        public int Next { get => _next; set => _next = value; }
        public int Trimin { get => _trimin; set => _trimin = value; }
        public int Trimout { get => _trimout; set => _trimout = value; }
        public string Artist { get => _artist; set => _artist = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
