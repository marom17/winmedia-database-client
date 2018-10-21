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
using System.Windows.Shapes;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Interaction logic for Progress.xaml
    /// </summary>
    public partial class Progress : Window
    {
        private ItemCollection files;
        private int max;
        private int actual;
        private ImportWindow parent;
        private readonly BackgroundWorker worker = new BackgroundWorker();

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.parent.Close();
            }
            catch (Exception) { }

        }

        public Progress(ItemCollection files, ImportWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            max = files.Count;
            actual = 0;
            this.PgBar.Maximum = max;
            this.TextDisp.Text = "0/" + max;
            this.files = files;
            this.Show();

            worker.DoWork += Transcode;
            worker.RunWorkerCompleted += Finished;
            worker.RunWorkerAsync(this.files);
            }

        public void Transcode(object sender, DoWorkEventArgs e)
        {
            
            foreach (Music file in (ItemCollection)e.Argument)
            {
                
                Debug.WriteLine(file);
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    this.TextDisp.Text = this.actual + "/" + this.max;
                    Debug.WriteLine(this.TextDisp.Text);
                });
                
                Transcoder.Encode(file, 0);

                AudioDB.ImportAudio(file);
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    this.actual += 1;
                    this.PgBar.Value = this.actual;
                    this.TextDisp.Text = this.actual + "/" + this.max;
                });

                Thread.Sleep(500);

            }
        }

        private void Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
