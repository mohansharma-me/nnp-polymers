using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NNPPoly
{
    public class Datastore
    {
        public static DataFile dataFile;
        public static String localPath;
        public static String DataFile { get { return "data.bin"; } }
        public static UserAccount current = null;

        public static object DownloadString(String url)
        {
            try
            {
                WebClient webClient = new WebClient();
                String retTN = webClient.DownloadString(url);
                return retTN;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static bool isEmail(String emailAddress)
        {
            bool flag = false;
            String[] arr1 = emailAddress.Split(new String[] { "@" }, StringSplitOptions.None);
            if (arr1.Length == 2)
            {
                String[] arr2 = arr1[1].Split(new String[] { "." }, StringSplitOptions.None);
                if (arr2.Length == 2)
                    flag = true;
            }
            return flag;
        }

        public static Exception SendMail(String from, String to, String subject, String html, String username, String password)
        {
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            MailMessage msg = null;

            try
            {
                msg = new MailMessage();
                msg.From = new MailAddress(from);
                msg.To.Add(to);
                msg.Subject = subject;
                msg.Body = html;

                msg.IsBodyHtml = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential(username, password);
                sc.EnableSsl = true;
                sc.Send(msg);
                return null;
            }

            catch (Exception ex)
            {
                return ex;
            }

            finally
            {
                if (msg != null)
                {
                    msg.Dispose();
                }
            }
        }
    }

    public class CMBItem
    {
        public String Name;
        public object Value;

        public CMBItem() { }
        public CMBItem(String nm, String vl)
        {
            Name = nm;
            Value = vl;
        }
        public CMBItem(String nm, object val)
        {
            Name = nm;
            Value = val;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
