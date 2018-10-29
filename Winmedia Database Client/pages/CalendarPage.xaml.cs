using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        private PlaylistShow plShow;
        private String selectedHour = "00h";
        private MainWindow parent;

        public CalendarPage(PlaylistShow plShow, MainWindow parent)
        {
            this.parent = parent;
            this.plShow = plShow;

            InitializeComponent();
            //this.PlaylistDate.SelectedDate = DateTime.Today;

            for (int i = 0; i < 24; i++)
            {
                TextBlock text = new TextBlock();
                text.Text = String.Format("{0:00}h",i);
                text.Background = Brushes.WhiteSmoke;
                text.MouseLeftButtonUp += Hour_Click;
                text.Margin = new Thickness(5,0,5,0);

                this.HourPanel.Children.Add(text);

            }
            ((TextBlock)this.HourPanel.Children[0]).Background = Brushes.Gray;
        }

        private void PlaylistDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date;
            try
            {
                date = (DateTime)this.PlaylistDate.SelectedDate;
            }
            catch (Exception)
            {
                Console.WriteLine("No default date");
                date = DateTime.Today;
            }
            DBHelper.connect();
            int idPlaylist = DBHelper.getPlaylistId(date, this.selectedHour);
            List<PlaylistElement> tmp = DBHelper.getPlaylist(idPlaylist);
            this.plShow.ShowPlaylist(tmp);
            DBHelper.disconnect();
        }

        private void Hour_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            try
            {
                date = (DateTime)this.PlaylistDate.SelectedDate;
            }
            catch (Exception)
            {
                Console.WriteLine("No default date");
                date = DateTime.Today;
            }
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                foreach (TextBlock item in this.HourPanel.Children)
                {
                    item.Background = Brushes.WhiteSmoke;
                }

                TextBlock block = ((TextBlock)sender);
                block.Background = Brushes.Gray;
            }));
            this.selectedHour = ((TextBlock)sender).Text;

            DBHelper.connect();
            int idPlaylist = DBHelper.getPlaylistId(date, this.selectedHour);
            List<PlaylistElement> tmp = DBHelper.getPlaylist(idPlaylist);
            this.plShow.ShowPlaylist(tmp);
            DBHelper.disconnect();
        }

        private void PlaylistDate_GotMouseCapture(object sender, MouseEventArgs e)
        {
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement is CalendarDayButton || originalElement is CalendarItem)
            {
                originalElement.ReleaseMouseCapture();
            }
        }
    }
}
