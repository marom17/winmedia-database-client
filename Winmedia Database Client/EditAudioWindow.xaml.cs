using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFSoundVisualizationLib;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour EditAudioWindow.xaml
    /// </summary>
    public partial class EditAudioWindow : Window
    {
        private TimeSpan position;
        NAudioEngine soundEngine = NAudioEngine.Instance;
        public EditAudioWindow()
        {
            InitializeComponent();
            this.waveformTimeline.RegisterSoundPlayer(soundEngine);
            soundEngine.OpenFile(@"C:\Program Files (x86)\Steam\steamapps\common\Europa Universalis IV\soundtrack\OST\theageofdiscovery.mp3");
            soundEngine.PropertyChanged += NAudioEngine_PropertyChanged;
            this.Show();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            NAudioEngine.Instance.Dispose();
        }

        private void NAudioEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NAudioEngine engine = NAudioEngine.Instance;
            Debug.WriteLine(e.PropertyName);
            switch (e.PropertyName)
            {
                case "ChannelPosition":
                    this.position = TimeSpan.FromSeconds(engine.ChannelPosition);
                    this.AudioTime.Text = this.position.ToString();
                    break;
            }

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (NAudioEngine.Instance.CanPlay)
            {
                NAudioEngine.Instance.Play();
                this.PlayButton.IsEnabled = false;
                this.StopButton.IsEnabled = true;
                this.PauseButton.IsEnabled = true;
            } 
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (NAudioEngine.Instance.CanPause)
            {
                NAudioEngine.Instance.Pause();
                this.PlayButton.IsEnabled = true;
                this.StopButton.IsEnabled = true;
                this.PauseButton.IsEnabled = false;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (NAudioEngine.Instance.CanStop)
            {
                NAudioEngine.Instance.Stop();
                this.PlayButton.IsEnabled = true;
                this.StopButton.IsEnabled = false;
                this.PauseButton.IsEnabled = false;
            } 
        }
    }
}
