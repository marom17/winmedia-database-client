using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{
    class DragDropHelper
    {
        private static ListViewItem dragSource;

        public static ListViewItem DragSource { get => dragSource; set => dragSource = value; }

    }
}
