using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
            this.VLCPath.Text = Config.VlcPath;
            this.DBHost.Text = Config.DBHost;
            this.DBPort.Text = Config.DBPort;
            this.DBUser.Text = Config.DBUser;
            this.DBPass.Password = Config.DBPass;
            this.DBTable.Text = Config.DB;

            this.Show();
        }

        private void SearchVLC_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "VLC|vlc.exe";
            if (openFileDialog.ShowDialog() == true)
            {
                this.VLCPath.Text = openFileDialog.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Config.saveConfig(this);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
