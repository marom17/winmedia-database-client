using System;
using System.Collections.Generic;
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

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        public ImportWindow(List<String> files)
        {
            InitializeComponent();
            foreach(var file in files)
            {
                this.ListFiles.Items.Add(new Music(file));
            }

            this.Show();
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(String file in this.ListFiles.Items)
            {
                Debug.WriteLine("Start Transcoding");
                Debug.WriteLine(file);
                Transcoder.Encode(file);
            }
            this.Close();
        }
    }
}
