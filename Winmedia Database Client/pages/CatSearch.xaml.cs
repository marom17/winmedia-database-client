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
    /// Logique d'interaction pour CatSearch.xaml
    /// </summary>
    public partial class CatSearch : Page
    {
        public CatSearch()
        {
            InitializeComponent();
            this.CatTab.Items.Add(new TabItem());
        }
    }
}
