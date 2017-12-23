using Provaders.DAL.Operator;
using Provaders.DAL.Operator.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Provaders.DAL.Administrator
{
    public class Administator
    {
        public Administator()
            : this("")
        {

        }
        public Administator(string pathToSave)
        {
            this.pathToSave = pathToSave;
        }
        private string pathToSave { get; set; }


        public bool CreateOperator(IOperator op, out string masseg)
        {
            masseg = "";
            if (!string.IsNullOrEmpty(pathToSave))
            {
                XmlDocument xDoc = GetXmlOperator();

                XmlElement rootElement =
                    xDoc.DocumentElement;
                //xDoc.CreateElement("operators");

                XmlElement rootOperatorElement =
                   xDoc.CreateElement("operator");

                XmlElement preficsesElem =
                    xDoc.CreateElement("preficses");

                foreach (Prefics item in op.prefics)
                {
                    XmlElement prefElem =
                    xDoc.CreateElement("pref");
                    prefElem.InnerText = item.pref.ToString();
                    preficsesElem.AppendChild(prefElem);
                }
                rootOperatorElement.AppendChild(preficsesElem);

                XmlElement logoElem =
                xDoc.CreateElement("logo");
                logoElem.InnerText = op.logo;
                rootOperatorElement.AppendChild(logoElem);

                XmlElement nameOperatorElem =
                xDoc.CreateElement("nameOperator");
                nameOperatorElem.InnerText = op.nameOperator;
                rootOperatorElement.AppendChild(nameOperatorElem);

                XmlElement procentElem =
                xDoc.CreateElement("procent");
                procentElem.InnerText = op.procent.ToString();
                rootOperatorElement.AppendChild(procentElem);

                rootElement.AppendChild(rootOperatorElement);
                try
                {
                    xDoc.Save(string.Format("{0}/operators.xml", pathToSave));
                    return true;
                }
                catch (Exception ex)
                {
                    masseg = ex.Message;
                    return false;
                }
            }
            else
            {
                masseg = "Место для сохранения не определено!";
                return false;
            }

        }

        public XmlDocument GetXmlOperator()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(string.Format("{0}/operators.xml", pathToSave));
            
            return xDoc;
        }
    }
}
