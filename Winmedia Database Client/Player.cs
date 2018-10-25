using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            try
            {
                audioFile = new AudioFileReader(path.ToString());
                wave.Init(audioFile);
            }
            catch (Exception) { }
            
        }

        public static void Play()
        {
            try
            {
                wave.Play();
            }
            catch (Exception) {
                MessageBox.Show("File not found!", "Error");
                wave.Dispose();
            }
            
        }

        public static void Stop()
        {
            try
            {
                wave.Stop();
            }
            catch (Exception) { }
        }

        public static void Resume()
        {
            wave.Resume();
        }

        public static TimeSpan Time()
        {
            return audioFile.CurrentTime;
        }

        public static double Reposition(double seconds)
        {
            audioFile.CurrentTime = TimeSpan.FromSeconds(seconds);
            return seconds;
        }

        public static void Dispose()
        {
            try
            {
                wave.Dispose();
            }
            catch (Exception) { }
            
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
