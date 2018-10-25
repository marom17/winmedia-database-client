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
using Winmedia_Database_Client.helpers;

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

        public MainWindow()
        {
            this.Hide();
            InitializeComponent();
            this.dBDisplay = new DBDisplay(this);
            this.DBDisplay.Navigate(this.dBDisplay);
            this.RightFrame.Navigate(new PlayerWindow());
            this.CatFrame.Navigate(new CatSearch(this.dBDisplay));
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
