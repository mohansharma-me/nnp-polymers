using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NNPPoly.classes.old_version
{
    public class eXML
    {
        public Exception Error = null;

        public eXML()
        {
            PathAppend = "";
        }

        public String PathAppend { get; set; }

        public bool Read<T>(String filename, Type typeClass, ref T obj, bool decrypt)
        {
            try
            {
                filename = PathAppend + filename;
                if (!File.Exists(filename))
                {
                    Error = new Exception("File not found");
                    return false;
                }

                String xmlcontent = File.ReadAllText(filename);
                if (decrypt)
                {

                }
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlcontent));
                XmlSerializer xs = new XmlSerializer(typeClass);
                obj = (T)xs.Deserialize(ms);

                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                StreamWriter sw = null;
                if (File.Exists("rlog.txt"))
                    sw = File.AppendText("rlog.txt");
                else
                    sw = File.CreateText("rlog.txt");
                sw.Write(Environment.NewLine + ex);
                sw.Close();
                return false;
            }
        }

        public bool Write(String filename, Type typeClass, object outObj, bool encrypt)
        {
            try
            {
                filename = PathAppend + filename;
                XmlSerializer xs = new XmlSerializer(typeClass);
                MemoryStream ms = new MemoryStream();
                xs.Serialize(ms, outObj);
                String xmlcontent = Encoding.UTF8.GetString(ms.ToArray());
                if (encrypt)
                {

                }
                FileStream afs = new FileStream(filename, FileMode.Create);
                byte[] bytes = Encoding.UTF8.GetBytes(xmlcontent);//Zip.doZip(xmlcontent));
                afs.Write(bytes, 0, bytes.Length);
                afs.Flush();
                afs.Close();
                return true;
            }
            catch (Exception er)
            {
                Error = er;
                StreamWriter sw = null;
                if (File.Exists("log.txt"))
                    sw = File.AppendText("log.txt");
                else
                    sw = File.CreateText("log.txt");
                sw.Write(Environment.NewLine + er);
                sw.Close();
                return false;
            }
            finally
            {

            }
        }
    }
}
