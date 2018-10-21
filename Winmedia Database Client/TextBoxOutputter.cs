using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Winmedia_Database_Client
{
    class TextBoxOutputter : TextWriter
    {
        TextBox textBox = null;

        public TextBoxOutputter(TextBox output)
        {
            textBox = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            textBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.AppendText(value.ToString());
            }));
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
