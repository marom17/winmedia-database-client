using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Winmedia_Database_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ConfigWindow _cfgWin;
        private ImportWindow _imWin;

        TextBoxOutputter outputter;

        public MainWindow()
        {
            InitializeComponent();

            outputter = new TextBoxOutputter(TestBox);
            Console.SetOut(outputter);
            Console.WriteLine("Started");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                _cfgWin.Close();
            }
            catch (Exception) { }

            try
            {
                _imWin.Close();
            }
            catch (Exception) { }

            base.OnClosing(e);
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<String> toImport = new List<string>();
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
                        toImport.Add(file);
                        AudioInfo.Info(file);
                    }
                }
                if (toImport.Count > 0)
                {
                    _imWin = new ImportWindow(toImport);
                }
                
            }
        }

        private void Rectangle_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ConfigBut_Click(object sender, RoutedEventArgs e)
        {
            _cfgWin = new ConfigWindow();
        }
    }
}
