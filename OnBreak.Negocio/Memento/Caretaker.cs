using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OnBreak.Negocio.Memento
{
    public class Caretaker
    {
        public bool RespaldarContrato(Contrato contrato)
        {
            string ObjContrato = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(contrato.GetType());
                StringWriter writer = new StringWriter();
                serializer.Serialize(writer, contrato);
                ObjContrato = writer.ToString();
                Memento.SaveState(ObjContrato);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Contrato RecuperarContrato()
        {
            Contrato contrato = new Contrato();
            string ObjContrato = null;
            object ObjetoConcreto = new object();
            ObjContrato = Memento.SetState();
            if (!String.IsNullOrEmpty(ObjContrato))
            {
                XmlSerializer serializer = new XmlSerializer(contrato.GetType());
                StringReader reader = new StringReader(ObjContrato);
                contrato = (Contrato)serializer.Deserialize(reader);
                return contrato;
            }
            return null;
        }
    }
}