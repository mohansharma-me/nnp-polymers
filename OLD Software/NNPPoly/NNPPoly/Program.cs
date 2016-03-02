﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Microsoft.Win32;

namespace NNPPoly
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception excep = e.ExceptionObject as Exception;
                if (excep != null)
                {
                    //DialogResult diag = MessageBox.Show("Unknown error generated by software, please see log file in installation folder or contact developer team with this error message:" + Environment.NewLine + Environment.NewLine + excep, "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        String errorLogFile = Application.ExecutablePath + ".log".ToLower();
                        StreamWriter sw = new StreamWriter(File.Open(errorLogFile,FileMode.Append));
                        sw.WriteLine(""); sw.WriteLine("");
                        sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt <==="));
                        sw.WriteLine(""+excep);
                        sw.WriteLine("===>");
                    }
                    catch (Exception) { }
                }
            };

            SystemSDFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            LogAddress = Application.ExecutablePath + ".log";
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Datastore.localPath = Application.LocalUserAppDataPath + "/";
            Datastore.localPath = Application.StartupPath + "/";

            SplashScreen ss = new SplashScreen();
            ss.ShowDialog();
            bool actFlag = false;
            if (File.Exists(Application.ExecutablePath + ".act"))
            {
                String data = File.ReadAllText(Application.ExecutablePath + ".act");
                if (data.Trim().Length != 0)
                {
                    try
                    {
                        data = StringSecurity.Decrypt(data, "9722505033");
                        if (data.Trim().Equals(File.GetCreationTime(Application.ExecutablePath).ToOADate().ToString().Trim()))
                        {
                            actFlag = true;
                        }
                    }
                    catch (Exception) {
                        Log.output("Activation error!!");
                    }
                }
            }

            if (!actFlag)
            {
                Activation activation = new Activation();
                activation.ShowDialog();
                //if (!activation.Suc) return;
            }

            Application.Run(new Main());
        }

        public static String SystemSDFormat = "";
        public static String LogAddress = "";
    }

    
}