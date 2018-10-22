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
        private static AudioFileReader audioFile;

        public static void Load(String path)
        {
            try
            {
                audioFile.Close();
            }
            catch (NullReferenceException) { }

            audioFile = new AudioFileReader(path.ToString());
            wave.Init(audioFile);
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

        public static double Time()
        {
            return audioFile.CurrentTime.TotalSeconds;
        }

        public static double Reposition(double seconds)
        {
            audioFile.CurrentTime = TimeSpan.FromSeconds(seconds);
            return seconds;
        }

        public static void Dispose()
        {
            wave.Dispose();
        }

        public static float Volume()
        {
            return audioFile.Volume;
        }

        public static double TotalTime()
        {
            return audioFile.TotalTime.TotalSeconds;
        }
    }
}
