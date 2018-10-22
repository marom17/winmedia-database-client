using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Winmedia_Database_Client
{ 
    public class LevelHelper
    {

        public static SolidColorBrush getColor(float peak)
        {
            if (peak < 80)
            {
                return new SolidColorBrush(Colors.ForestGreen);
            }
            else if (peak < 95)
            {
                return new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                return new SolidColorBrush(Colors.Red);
            }
        }
    }
}