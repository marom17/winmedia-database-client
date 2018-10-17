using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    class Transcoder
    {
        private static String[] _options = { "--no-repeat --no-loop -I dummy --dummy-quiet \"", "\" vlc://quit --sout=#transcode{acodec=mp2,ab=384,samplerate=48000,channels=2}:standard{access=file,mux=ts,dst=\"", "\" }" };

        public static void Encode(String path)
        {
            String dest = "./test.sam";

            String arg = _options[0] + path + _options[1] + dest + _options[2];

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = arg;
            // Enter the executable to run, including the complete path
            start.FileName = Config.VlcPath;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            Debug.WriteLine(Config.VlcPath);

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

        }
    }
}
