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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private Progress _pgWin;

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
            Player.Load(item.FilePath);
            this.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                Player.Stop();
            }
            catch (Exception) { }

            base.OnClosed(e);
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start Transcoding");
            Console.WriteLine("Start Transcoding");
            try
            {
                _pgWin = new Progress(this.ListFiles.Items,this);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                try
                {
                    Player.Stop();
                }
                catch (Exception) { }

                Player.Load(item.FilePath);
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

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            Player.Play();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void ListFiles_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
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
                    foreach (var file in toImport)
                    {
                        this.ListFiles.Items.Add(new Music(file));
                    }
                }

            }
        }
    }
}
