using Provaders.DAL.Operator;
using Provaders.DAL.Operator.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        public bool CreateOperatorSerialize(Operator.Operator op, out string masseg)
        {
            XmlSerializer formatter =
                    new XmlSerializer(typeof(Operator.Operator[]));

            List<Operator.Operator> opList = new List<Operator.Operator>();

            #region 1. Найти файл с операторами (Desirialize)
            try
            {
                using (FileStream fs =
                                    new FileStream(
                                        string.Format("{0}/operators.xml", pathToSave),
                                        FileMode.OpenOrCreate))
                {
                    opList = 
                        ((Operator.Operator[])formatter.Deserialize(fs)).ToList();
                }
            }
            catch (Exception)
            {
            }

            #endregion

            #region 2. Добавить в Operator операторов
            opList.Add(op);
            #endregion

            #region 3. Среиализировать весь объект 
            using (FileStream fs =
                   new FileStream(
                       string.Format("{0}/operators.xml", pathToSave),
                       FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, opList.ToArray());
            }
            #endregion

            masseg = "ok";
            return true;
        }





        public void DoSerialize()
        {
            Operator.Operator op = new Operator.Operator();
            op.logo = "logoHear";
            op.procent = 2;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs =
                new FileStream("operator.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, op);
                fs.Close();
            }
        }

        public Operator.Operator RedUSerialize()
        {
            Operator.Operator op = null;
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = 
                new FileStream("operator.dat", FileMode.OpenOrCreate))
            {
                  op =
                    (Operator.Operator)formatter.Deserialize(fs);
            }

            return op;
        }

        public void DoSoapSerialize()
        {
            List<Operator.Operator> opList = new List<Operator.Operator>();
            
            Operator.Operator op = new Operator.Operator();
            op.logo = "logoHear1";
            op.procent = 1;

            Operator.Operator op2 = new Operator.Operator();
            op2.logo = "logoHear2";
            op2.procent = 2;

            Operator.Operator op3 = new Operator.Operator();
            op3.logo = "logoHear3";
            op3.procent = 3;


            opList.Add(op);
            opList.Add(op2);
            opList.Add(op3);



            SoapFormatter sformatter = new SoapFormatter();
            using (FileStream fs = 
                new FileStream("sopaFileOperator.xml", FileMode.OpenOrCreate))
            {
                sformatter.Serialize(fs, opList.ToArray());
            }
        }

        public Operator.Operator[] ReDoSoapSerialize()
        {
            Operator.Operator[] ops = null; 
            SoapFormatter sformatter = new SoapFormatter();
            using (FileStream fs =
                new FileStream("sopaFileOperator.xml", FileMode.OpenOrCreate))
            {
                ops = 
                    (Operator.Operator[])sformatter.Deserialize(fs);
            }

            return ops;
        }

        public void DoXmlSerialize()
        {
            List<Operator.Operator> opList = new List<Operator.Operator>();

            Operator.Operator op = new Operator.Operator();
            op.logo = "logoHear1";
            op.procent = 1;

            Operator.Operator op2 = new Operator.Operator();
            op2.logo = "logoHear2";
            op2.procent = 2;

            Operator.Operator op3 = new Operator.Operator();
            op3.logo = "logoHear3";
            op3.procent = 3;


            opList.Add(op);
            opList.Add(op2);
            opList.Add(op3);


            XmlSerializer formatter = 
                new XmlSerializer(typeof(Operator.Operator[]));

            using (FileStream fs = 
                new FileStream("xmlFormatter.xml",
                FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, 
                    opList.ToArray());
            }
        }
    }
}
