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
    /// Interaction logic for PlaylistShow.xaml
    /// </summary>
    public partial class PlaylistShow : Page
    {
        public PlaylistShow()
        {
            InitializeComponent();
        }

        public void ShowPlaylist(List<PlaylistElement> playlist)
        {
            this.PlList.Items.Clear();

            foreach (var item in playlist)
            {
                ListBoxItem boxItem = new ListBoxItem();
                boxItem.Content = item;
                this.PlList.Items.Add(item);
            }
        }

        private void PlList_Drop(object sender, DragEventArgs e)
        {
            Music music = (Music)e.Data.GetData(typeof(Music));
            if (music != null)
            {
                ListBoxItem boxItem = new ListBoxItem();
                boxItem.Content = new PlaylistElement(music);
                this.PlList.Items.Add(boxItem);
            }
        }
    }
}
