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
            //base.Background = Brushes.LightGray;
            StackPanel grid = new StackPanel();
            grid.Background = Brushes.LightGray;

            foreach (var item in cat)
            {
                TextBlock catObj = new TextBlock();
                catObj.Text = (String)item[0];
                catObj.Name = "Cat" + item[1].ToString();
                catObj.MouseLeftButtonUp += Cat_Click;
                grid.Children.Add(catObj);
            }
            this.Content = grid;

        }

        private void Cat_Click(object sender, RoutedEventArgs e)
        {
            int catId = Convert.ToInt32(Regex.Match(((TextBlock)sender).Name, @"\d+").Value);

            this.dBDisplay.SearchMusic(catId);
        }
    }
}
