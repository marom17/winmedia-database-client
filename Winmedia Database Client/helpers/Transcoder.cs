using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winmedia_Database_Client
{
    internal class Transcoder
    {
        private static String[] _options = { "--no-repeat --no-loop -I dummy \"", "\" vlc://quit --sout=#transcode{acodec=mp2,ab=384,samplerate=48000,channels=2}:standard{access=file,mux=ts,dst=\"", "\" }" };

        public static Boolean Encode(Music file, int attempts)
        {
            String path = file.FilePath;
            
            var md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            var hash = md5.ComputeHash(inputBytes);
            var fileName = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant() + ".sam";

            String dest = @"import\" + fileName;

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

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
                Thread.Sleep(500);
                
                FileInfo f = new FileInfo(dest);
                if (f.Length == 0)
                {
                    f.Delete();
                    Debug.WriteLine("Error");
                    Debug.WriteLine("Retry");
                    if (attempts < 5)
                    {
                        Transcoder.Encode(file, attempts + 1);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    String nasDest = Config.FilePath + fileName;
                    try
                    {
                        File.Move(dest, nasDest);
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    
                    
                    file.FilePath = nasDest;
                    file.FileName = fileName;
                    return true;
                }
            }

            return true;

        }
    }
}
