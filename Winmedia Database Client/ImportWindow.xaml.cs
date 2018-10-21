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

            this.ListFiles.SelectedIndex = 0;
            var item = ((Music)(this.ListFiles.SelectedItem));
            this.SetInfo(item);
            this.Show();
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start Transcoding");
            Console.WriteLine("Start Transcoding");
            try
            {
                foreach (Music file in this.ListFiles.Items)
                {

                    Debug.WriteLine(file);
                    Transcoder.Encode(file);

                    AudioDB.ImportAudio(file);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            this.Close();
        }

        private void SetInfo(Music item)
        {
            this.FileName.Text = item.FileName;
            this.Artist.Text = item.Artist;
            this.Title.Text = item.Title;
            this.Duration.Text = item.Duration.ToString();
            this.Intro.Text = item.Intro.ToString();
            this.Next.Text = item.Next.ToString();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((Music)(sender as ListViewItem).Content);
            if (item != null)
            {
                this.SetInfo(item);
            }
        }

        private void SaveBnt_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = ((Music)(this.ListFiles.SelectedItem));

            item.FileName = this.FileName.Text;
            item.Artist = this.Artist.Text;
            item.Title = this.Title.Text;
            item.Duration = Convert.ToInt32(this.Duration.Text);
            item.Intro = Convert.ToInt32(this.Intro.Text);
            item.Next = Convert.ToInt32(this.Next.Text);

        }
    }
}
