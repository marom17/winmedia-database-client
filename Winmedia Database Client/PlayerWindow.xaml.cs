using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Page
    {
        private CancellationTokenSource ts;
        private Task getPeaks;
        private Boolean isPlaying = false;
        private TimeSpan Duration;
        private static Music playing;


        public PlayerWindow()
        {
            InitializeComponent();
            ts = new CancellationTokenSource();
            CancellationToken cs = ts.Token;
            getPeaks = new Task(() => {
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                var peaks = defaultDevice.AudioMeterInformation.PeakValues;
                float left = 0;
                float right = 0;

                Debug.WriteLine("Sending Volume level through default");
                while (!cs.IsCancellationRequested)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate () {
                        left = (peaks[0] * 100);
                        right = (peaks[1] * 100);

                        this.LeftBar.Foreground = LevelHelper.getColor(left);
                        this.RightBar.Foreground = LevelHelper.getColor(right);

                        this.LeftBar.Value = left;
                        this.RightBar.Value = right;

                        if (isPlaying)
                        {
                            TimeSpan pTime = Player.Time();
                            this.Timer.Text = String.Format("{0:2D}:{1:2D}/{2:2D}:{3:2D}", (int)pTime.TotalMinutes,pTime.Seconds,(int)Duration.TotalMinutes,Duration.Seconds);
                        }
                    });
                    Thread.Sleep(250);
                }
                
            },cs);
            getPeaks.Start();
            
        }

        public void Close()
        {
            try
            {
                ts.Cancel();
            }
            catch (Exception) { }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Player.Play();
            this.Duration = new TimeSpan(0, 0, playing.Duration);
            this.TBArtiste.Text = playing.Artist;
            this.TBTitle.Text = playing.Title;
            this.isPlaying = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            this.isPlaying = false;
        }

        public static void LoadFile(Music file)
        {
            playing = file;
            Player.Load(file.FilePath);
        }
    }
}
