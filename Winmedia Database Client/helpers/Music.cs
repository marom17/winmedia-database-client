using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{
    public class Music
    {
        private String _filePath;
        private String _artist;
        private String _title;
        private String _fileName;
        private int _fileLength;
        private int _timeLength; //Duration in Winmedia
        private int _duration; //Time of original file
        private int _start;
        private int _intro;
        private int _next;
        private int _trimin;
        private int _trimout; //Same as cutout
        private int _stop; // Different of trimout and cutout
        private int _cutout; //Same as trimout
        private TimeSpan _prettyTime;
        private TimeSpan _prettyIntro;

        private int _introin;
        private int _introout;
        private int _catid;
        private int _fadein;
        private int _fadeout;
        private int _jingle;
        private int _jingleposition;
        private int _jingleVolume;
        private double _stretch;
        private int _mediaId;

        public string FilePath { get => _filePath; set => _filePath = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public int Intro { get => _intro; set => _intro = value; }
        public int Next { get => _next; set => _next = value; }
        public int Trimin { get => _trimin; set => _trimin = value; }
        public int Trimout { get => _trimout; set => _trimout = value; }
        public string Artist { get => _artist; set => _artist = value; }
        public string Title { get => _title; set => _title = value; }
        public int Stop { get => _stop; set => _stop = value; }
        public int Cutout { get => _cutout; set => _cutout = value; }
        public int Start { get => _start; set => _start = value; }
        public string FileName { get => _fileName; set => _fileName = value; }
        public int FileLength { get => _fileLength; set => _fileLength = value; }
        public TimeSpan PrettyTime { get => _prettyTime; set => _prettyTime = value; }
        public int TimeLength { get => _timeLength; set => _timeLength = value; }
        public int Introin { get => _introin; set => _introin = value; }
        public int Introout { get => _introout; set => _introout = value; }
        public int Catid { get => _catid; set => _catid = value; }
        public int Jingle { get => _jingle; set => _jingle = value; }
        public int Jingleposition { get => _jingleposition; set => _jingleposition = value; }
        public int JingleVolume { get => _jingleVolume; set => _jingleVolume = value; }
        public int Fadein { get => _fadein; set => _fadein = value; }
        public int Fadeout { get => _fadeout; set => _fadeout = value; }
        public double Stretch { get => _stretch; set => _stretch = value; }
        public TimeSpan PrettyIntro { get => _prettyIntro; set => _prettyIntro = value; }
        public int MediaId { get => _mediaId; set => _mediaId = value; }

        public Music(object[] info)
        {
            _filePath = Config.FilePath + (String)info[3];
            _artist = (String)info[0];
            _title = (String)info[1];
            _timeLength = ((int)info[2]);
            _prettyTime = new TimeSpan(0, 0, (int)(_timeLength / 1000));
            _catid = (int)info[5];
            _start = (int)info[6];
            _stop = (int)info[7];
            _introin = (int)info[8];
            _introout = (int)info[9];
            _intro = _introout - _introin;
            _fadein = (int)info[10];
            _fadeout = (int)info[11];
            _jingle = (int)info[12];
            _jingleposition = (int)info[13];
            _jingleVolume = Convert.ToInt16(info[14]);
            _stretch = Convert.ToDouble(info[15]);
            _prettyIntro = new TimeSpan(0,0,(int)(_intro/1000));
            _mediaId = (int)info[16];

        }

        public Music(String uri)
        {
            Dictionary<String, String> info = AudioInfo.Info(uri);

            _filePath = uri;
            _artist = info["Artist"];
            _title = info["Title"];
            _fileName = info["FileName"];
            _fileLength = Convert.ToInt32(info["FileLength"]);
            _duration = Convert.ToInt32(info["Duration"]);
            _timeLength = Convert.ToInt32(info["Duration"]);
            _prettyTime = new TimeSpan(0, 0, _timeLength/1000);
            _intro = 0;
            _start = 0;
            _next = _duration;
            _trimin = 0;
            _trimout = _duration;
            _stop = _duration;
            _cutout = _duration;

        }

        public override string ToString()
        {
            return _fileName;
        }
    }
}
