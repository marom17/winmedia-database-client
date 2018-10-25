using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Winmedia_Database_Client.helpers
{
    class CatTab: TabItem
    {
        private DBDisplay dBDisplay;

        public CatTab(String header, List<object[]> cat, DBDisplay dBDisplay)
        {
            this.dBDisplay = dBDisplay;
            base.Header = header;
            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            WrapPanel grid = new WrapPanel();
            grid.Orientation = Orientation.Vertical;
            grid.Background = Brushes.LightGray;
            grid.MaxHeight = 160;

            scroll.Content = grid;
            foreach (var item in cat)
            {
                Rectangle rect = new Rectangle();
                TextBlock catObj = new TextBlock();
                catObj.Text = (String)item[0];
                catObj.Name = "Cat" + item[1].ToString();
                catObj.MouseLeftButtonUp += Cat_Click;
                catObj.FontSize = 12;
                catObj.Background = Brushes.WhiteSmoke;
                catObj.Margin = new Thickness(2, 2, 2, 2);
                catObj.Padding = new Thickness(5, 5, 5, 5);
                grid.Children.Add(catObj);
            }
            this.Content = scroll;

        }

        private void Cat_Click(object sender, RoutedEventArgs e)
        {
            int catId = Convert.ToInt32(Regex.Match(((TextBlock)sender).Name, @"\d+").Value);

            this.dBDisplay.SearchMusic(catId);
        }
    }
}
