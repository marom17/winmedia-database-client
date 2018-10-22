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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Logique d'interaction pour DBDisplay.xaml
    /// </summary>
    public partial class DBDisplay : Page
    {
        public DBDisplay()
        {
            InitializeComponent();

            new Task(() => {
                DBHelper.connect();
                List<Music> MusList = DBHelper.GetMusics(-1);
                foreach(var music in MusList)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        this.MusicList.Items.Add(music);
                    });
                }
                DBHelper.disconnect();
            }).Start();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((Music)(sender as ListViewItem).Content);
            if (item != null)
            {
                PlayerWindow.LoadFile(item);
            }
        }
    }
}
