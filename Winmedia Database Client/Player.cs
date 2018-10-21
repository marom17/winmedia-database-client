using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class Player
    {
        private static WaveOut wave = new WaveOut();

        public static void Load(String path)
        {
            var x = new AudioFileReader(path.ToString());
            wave.Init(x);
        }

        public static void Play()
        {
            wave.Play();
        }

        public static void Stop()
        {
            wave.Stop();
        }

        public static void Resume()
        {
            wave.Resume();
        }

        public static long Time()
        {
            return wave.GetPosition();
        }

        public static void Dispose()
        {
            wave.Dispose();
        }
    }
}
