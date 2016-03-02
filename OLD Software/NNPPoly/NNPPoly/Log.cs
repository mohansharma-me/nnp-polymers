using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NNPPoly
{
    public class Log
    {
        public static void output(Exception excep)
        {
            try
            {
                String errorLogFile = Program.LogAddress;
                StreamWriter sw = new StreamWriter(File.Open(errorLogFile, FileMode.Append));
                sw.WriteLine(""); sw.WriteLine("");
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt <==="));
                sw.WriteLine("" + excep);
                sw.WriteLine("===>");
            }
            catch (Exception) { }
        }
        public static void output(String message)
        {
            try
            {
                String errorLogFile = Program.LogAddress;
                StreamWriter sw = new StreamWriter(File.Open(errorLogFile, FileMode.Append));
                sw.WriteLine(""); sw.WriteLine("");
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt <==="));
                sw.WriteLine(message);
                sw.WriteLine("===>");
            }
            catch (Exception) { }
        }
        public static void output(String message,Exception excep)
        {
            try
            {
                message = Environment.NewLine + Environment.NewLine + message;
                message = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt <===")+Environment.NewLine+message;
                message += excep+Environment.NewLine+"===>";
                File.AppendAllText(Program.LogAddress, message);
            }
            catch (Exception) { }
        }

    }
}
