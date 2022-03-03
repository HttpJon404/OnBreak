using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnBreak.Negocio.Memento
{
    public class Memento
    {

        public static bool SaveState(string ObjContrato)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path = path + @"..\..\..\OnBreak.Negocio\Backup\Contrato.xml";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ObjContrato);
                doc.Save(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SetState()
        {
            string ObjSerializado;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = path + @"..\..\..\OnBreak.Negocio\Backup\Contrato.xml";
            ObjSerializado = File.ReadAllText(path);

            return ObjSerializado;
        }

    }
}
