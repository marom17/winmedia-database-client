using NAudio.Wave;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ConfigWindow _cfgWin;
        private ImportWindow _imWin;
        private DBDisplay dBDisplay;
        private PlaylistShow playlistShow;
        private EditAudioWindow editAudio;

        public MainWindow()
        {
            this.Hide();
            InitializeComponent();
            if (!Config.Init())
            {
                _cfgWin = new ConfigWindow();
            }
            else
            {
                try
                {
                    /*
                    this.dBDisplay = new DBDisplay(this);
                    this.DBDisplay.Navigate(this.dBDisplay);
                    Thread.Sleep(250);
                    this.RightFrame.Navigate(new PlayerWindow());
                    Thread.Sleep(250);
                    this.CatFrame.Navigate(new CatSearch(this.dBDisplay));
                    Thread.Sleep(250);
                    this.playlistShow = new PlaylistShow();
                    this.Calendar.Navigate(new CalendarPage(this.playlistShow, this));
                    Thread.Sleep(250);
                    this.Playlist.Navigate(this.playlistShow);*/
                    this.editAudio = new EditAudioWindow();
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    Console.WriteLine(ex.Message);
                }
                
            }            
            
            this.Show();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                _cfgWin.Close();
            }
            catch (Exception) { }

            try
            {
                _imWin.Close();
            }
            catch (Exception) { }

            try
            {
                editAudio.Close();
            }
            catch (Exception) { }

            base.OnClosing(e);
        }

        public void StartImport(string[] files)
        {
            List<String> toImport = new List<string>();
            foreach (var file in files)
            {
                bool formatOk = false;
                foreach (var fm in Config.Format)
                {
                    if (file.ToLower().Contains(fm))
                    {
                        formatOk = true;
                    }
                }
                if (formatOk)
                {
                    toImport.Add(file);
                }
            }
            if (toImport.Count > 0)
            {
                _imWin = new ImportWindow(toImport);
            }
        }

        private void ConfigBut_Click(object sender, RoutedEventArgs e)
        {
            _cfgWin = new ConfigWindow();
        }
    }
}
