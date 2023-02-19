using CryptoGetter;
using System;
using System.IO;
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

        public void GenerateXmlFiles (string xmlPath, string sgtinPath, string outerPath, string lot, string expiredTo)
        {
            if (xmlPath == "")   throw new Exception("Пустое имя файла XML!");
            if (sgtinPath == "") throw new Exception("Пустое имя файла c SGTIN!");
            if (outerPath == "") throw new Exception("Пустое поле выходной папки!");
            if (xmlPath == "")   throw new Exception("Пустое поле серии!");
            if (xmlPath == "")   throw new Exception("Пустое поле срока годности!");
            

            if (File.Exists(xmlPath) == false)   throw new Exception("Файл XML не найден!");
            if (File.Exists(sgtinPath) == false) throw new Exception("Файл с SGTIN не найден!");

            KIZ[] kizes = LoadSgtinsFromFile(sgtinPath);

            for (int i = 0; i < kizes.Length; i++)
            {
                (kizes[i].CryptoKey, kizes[i].CryptoCode) = _dm.GetCrypto(kizes[i].SGTIN);
            }

            BuildXmlFiles(ref kizes, xmlPath, outerPath, lot, expiredTo);
        }

        private KIZ[] LoadSgtinsFromFile(string path)
        {
            var sgtins = File.ReadAllLines(path);
            if (sgtins.Length == 0) throw new Exception("SGTIN не найдены!");
            
            KIZ[] result = new KIZ[sgtins.Length];
            
            for (int i = 0; i < sgtins.Length; i++)
            {
                result[i].SGTIN = sgtins[i];
                result[i].GTIN = result[i].SGTIN.Substring(0,14);
                result[i].Serial = result[i].SGTIN.Substring(14, 13);
            }
            
            return result;
        }
    
        private void BuildXmlFiles(ref KIZ[] kizes, string xmlPath, string outerPath, string lot, string expiredTo)
        {
            XmlDocument xDoc = new XmlDocument();

            for (int i = 0; i < kizes.Length; i++)
            {
                xDoc.LoadXml(File.ReadAllText(xmlPath));

                var printLines = xDoc.SelectNodes("/job/printObjects/printObject/printLines/printLine");

                foreach (XmlNode line in printLines)
                {
                    if (line.InnerXml.Contains("fixedText")) continue;
                    
                    if (line.InnerXml.Contains("GTIN")) 
                    { 
                        line.RemoveAll();
                        line.InnerXml = $"<fixedText>{kizes[i].GTIN}</fixedText>";
                    }

                    if (line.InnerXml.Contains("Serial"))
                    {
                        line.RemoveAll();
                        line.InnerXml = $"<fixedText>{kizes[i].Serial}</fixedText>";
                    }
                    
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

                var dataMatrix = xDoc.SelectSingleNode("/job/printObjects/printObject/sourceLine");
                dataMatrix.RemoveAll();
                dataMatrix.InnerXml = $"01{kizes[i].GTIN}21{kizes[i].Serial}<gs1GroupSeparator>29</gs1GroupSeparator>" +
                    $"91{kizes[i].CryptoKey}<gs1GroupSeparator>29</gs1GroupSeparator>92{kizes[i].CryptoCode}";

                xDoc.Save(outerPath + "\\" + kizes[i].SGTIN + ".xml");
            }
        }
    }
}
