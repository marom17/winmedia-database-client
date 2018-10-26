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
        private String selectedHour = "0h";

        public CalendarPage(PlaylistShow plShow)
        {
            this.plShow = plShow;

            InitializeComponent();

            for(int i = 0; i < 24; i++)
            {
                TextBlock text = new TextBlock();
                text.Text = i + "h";
                text.Background = Brushes.WhiteSmoke;
                text.MouseLeftButtonUp += Hour_Click;
                text.Margin = new Thickness(5,0,5,0);

                this.HourPanel.Children.Add(text);

            }
            ((TextBlock)this.HourPanel.Children[0]).Background = Brushes.Gray;
        }

        private void PlaylistDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(this.PlaylistDate.SelectedDate);
            Debug.WriteLine(this.selectedHour);
        }

        private void Hour_Click(object sender, RoutedEventArgs e)
        {
            foreach(TextBlock item in this.HourPanel.Children)
            {
                item.Background = Brushes.WhiteSmoke;
            }

            TextBlock block = ((TextBlock)sender);
            Debug.WriteLine(block.Text);
            block.Background = Brushes.Gray;

            this.selectedHour = block.Text;
        }
    }
}
