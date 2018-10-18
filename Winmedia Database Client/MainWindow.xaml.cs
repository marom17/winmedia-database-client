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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach(var file in files){
                    bool formatOk = false;
                    foreach(var fm in Config.Format)
                    {
                        if (file.Contains(fm))
                        {
                            formatOk = true;
                        }
                    }
                    if (formatOk)
                    {
                        Debug.WriteLine("Start Transcoding");
                        Transcoder.Encode(file);
                    }
                    Debug.WriteLine(file);
                }
                
            }
        }

        private void Rectangle_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ConfigBut_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cfgWin = new ConfigWindow();
        }
    }
}
