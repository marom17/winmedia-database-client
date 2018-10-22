using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    public class Music
    {
        private String _filePath;
        private String _artist;
        private String _title;
        private String _fileName;
        private int _fileLength;
        private int _timeLength;
        private int _duration;
        private int _start;
        private int _intro;
        private int _next;
        private int _trimin;
        private int _trimout; //Same as cutout
        private int _stop; // Different of trimout and cutout
        private int _cutout; //Same as trimout
        private TimeSpan _prettyTime;

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

        public Music(object[] info)
        {
            _filePath = Config.FilePath + (String)info[3];
            _artist = (String)info[0];
            _title = (String)info[1];
            //_fileName = info["FileName"];
            //_fileLength = Convert.ToInt32(info["FileLength"]);
            _timeLength = ((int)info[2]);
            _prettyTime = new TimeSpan(0, 0, (int)(_timeLength / 1000));
            //_prettyDuration = new TimeSpan(0, 0, _duration);
            //_intro = 0;
            //_start = 0;
            //_next = _duration;
            //_trimin = 0;
            //_trimout = _duration;
            //_stop = _duration;
            //_cutout = _duration;
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
