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


        public PlayerWindow()
        {
            InitializeComponent();
            ts = new CancellationTokenSource();
            CancellationToken cs = ts.Token;
            getPeaks = new Task(() => {
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                var peaks = defaultDevice.AudioMeterInformation.PeakValues;

                Debug.WriteLine("Sending Volume level through default");
                while (!cs.IsCancellationRequested)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate () {
                        this.LeftBar.Value = (int)(peaks[0]*100);
                        this.RightBar.Value = (int)(peaks[1] * 100);
                    });
                    Debug.WriteLine("\rCurrent Level: " + peaks[0] + "/" + peaks[1]);
                    Thread.Sleep(500);
                }
                Debug.WriteLine("Task completed");
                
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
    }
}
