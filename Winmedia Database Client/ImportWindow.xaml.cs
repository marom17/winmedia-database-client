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

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((Music)(sender as ListViewItem).Content);
            if (item != null)
            {
                this.FileName.Text = item.FileName;
                this.Artist.Text = item.Artist;
                this.Title.Text = item.Title;
                this.Duration.Text = item.Duration.ToString();
                this.Intro.Text = item.ToString();
                this.Next.Text = item.Next.ToString();
            }
        }
    }
}
