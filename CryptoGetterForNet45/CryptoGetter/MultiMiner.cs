using CryptoGetter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace CryptoGetterForNet45.CryptoGetter
{
    internal class MultiMiner
    {
        private readonly IDataMiner _dm;

        struct KIZ
        {
            public string SGTIN;
            public string GTIN;
            public string Serial;
            public string CryptoKey;
            public string CryptoCode;
        }

        public MultiMiner(IDataMiner dataminer)
        {
            _dm = dataminer;
        }

        public void GenerateXmlFiles (string xmlPath, string sgtinPath, string lot, string expiredTo)
        {
            if (xmlPath == "") throw new Exception("Пустое имя файла XML!");
            if (sgtinPath == "") throw new Exception("Пустое имя файла c SGTIN!");
            if (xmlPath == "") throw new Exception("Пустое поле серии!");
            if (xmlPath == "") throw new Exception("Пустое поле срока годности!");

            if (File.Exists(xmlPath) == false) throw new Exception("Файл XML не найден!");
            if (File.Exists(sgtinPath) == false) throw new Exception("Файл с SGTIN не найден!");

            KIZ[] kizes = LoadSgtinsFromFile(sgtinPath);

            for (int i = 0; i < kizes.Length; i++)
            {
                (kizes[i].CryptoKey, kizes[i].CryptoCode) = _dm.GetCrypto(kizes[i].SGTIN);
            }

            BuildXmlFiles(ref kizes, xmlPath, lot, expiredTo);

        }


        private KIZ[] LoadSgtinsFromFile(string path)
        {
            var sgtins = File.ReadAllLines(path);
            if (sgtins.Length == 0) throw new Exception("SGTIN не найдены!");
            
            KIZ[] result = new KIZ[sgtins.Length];
            
            for (int i = 0; i < sgtins.Length; i++)
            { 
                result[i].SGTIN = sgtins[i].Trim('\r', '\n');
                result[i].GTIN = result[i].SGTIN.Substring(0,13);
                result[i].Serial = result[i].SGTIN.Substring(14, 27);
            }
            
            return result;
        }

        private void GetCryptoData(ref KIZ[] kizes)
        {

        }
    
        private void BuildXmlFiles(ref KIZ[] kizs, string xmlPath, string lot, string expiredTo)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(File.ReadAllText(xmlPath));

            var printLines = xDoc.SelectNodes("/job/printObjects/printObject/printLines/printLine");

            foreach (XmlNode line in printLines)
            {
                if (line.InnerXml.Contains("fixedText")) continue;
                /*
                if (line.InnerXml.Contains("GTIN")) 
                { 
                    line.RemoveAll();
                    line.InnerXml = $"<fixedText>{gtin}</fixedText>";
                }

                if (line.InnerXml.Contains("Serial"))
                {
                    line.RemoveAll();
                    line.InnerXml = $"<fixedText>{serial}</fixedText>";
                }
                */
                if (line.InnerXml.Contains("Lot"))
                {
                    line.RemoveAll();
                    line.InnerXml = $"<fixedText>{lot}</fixedText>";
                }

                if (line.InnerXml.Contains("Eval"))
                {
                    line.RemoveAll();
                    line.InnerXml = $"<fixedText>{expiredTo}</fixedText>";
                }
            }


            //xDoc.WriteContentTo(XmlWriter xw);
            XmlDocument preparedXdoc = new XmlDocument();
            preparedXdoc.LoadXml(xDoc.ToString());
            preparedXdoc.Save(xmlPath.Insert(xmlPath.Length - 4, "2"));

        }
    }
}
