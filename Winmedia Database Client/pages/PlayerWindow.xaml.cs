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
        private TimeSpan Duration;
        private static Music playing;
        private Boolean slideIsGrab = false;

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
                
                while (!cs.IsCancellationRequested)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate () {
                        left = (peaks[0] * 100);
                        right = (peaks[1] * 100);

                        this.LeftBar.Foreground = LevelHelper.getColor(left);
                        this.RightBar.Foreground = LevelHelper.getColor(right);

                        this.LeftBar.Value = left;
                        this.RightBar.Value = right;

                        try{
                            try
                            {
                                TimeSpan pTime = Player.Time();
                                this.Timer.Text = String.Format("{0:00}:{1:00}/{2:00}:{3:00}", (int)pTime.TotalMinutes, (double)pTime.Seconds, (int)Duration.TotalMinutes, (double)Duration.Seconds);
                                if (!this.slideIsGrab)
                                {
                                    this.Time.Value = pTime.TotalSeconds;
                                }
                            }
                            catch (Exception) {
                                this.Timer.Text = String.Format("00:00/{0:00}:{1:00}", (int)Duration.TotalMinutes, (double)Duration.Seconds);
                                this.Time.Value = 0;
                            }
                            
                            this.Duration = playing.PrettyTime;
                            this.TBArtiste.Text = playing.Artist;
                            this.TBTitle.Text = playing.Title;
                        }
                        catch (Exception) { }
                        
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
            try
            {
                Player.Play();
                this.Time.Maximum = this.Duration.TotalSeconds;
                this.Time.Value = 0;
            }
            catch (Exception) { }
            
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        public static void LoadFile(Music file)
        {
            playing = file;
            try
            {
                Player.Stop();
            }
            catch (Exception) { }
            Player.Load(file.FilePath);
        }

        private void Time_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.slideIsGrab = true;
        }

        private void Time_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                Player.Reposition(this.Time.Value);
            }
            catch (Exception) { }

            this.slideIsGrab = false;
        }
    }
}
