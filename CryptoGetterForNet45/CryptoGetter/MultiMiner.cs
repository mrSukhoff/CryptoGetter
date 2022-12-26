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
        private readonly Server _server;

        public MultiMiner(Server server)
        {
            _server = server;
        }

        public void GenerateXmlFiles (string xmlPath, string sgtinPath, string lot, string expiredTo)
        {
            if (xmlPath == "") throw new Exception("Пустое имя файла XML!");
            //if (sgtinPath == "") throw new Exception("Пустое имя файла c SGTIN!");
            //if (xmlPath == "") throw new Exception("Пустое поле серии!");
            //if (xmlPath == "") throw new Exception("Пустое поле срока годности!");

            if (File.Exists(xmlPath) == false) throw new Exception("Файл XML не найден!");
            //if (File.Exists(sgtinPath) == false) throw new Exception("Файл с SGTIN не найден!");

            //var sgtins = LoadSgtins(sgtinPath);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(File.ReadAllText(xmlPath));

            var printLines = xDoc.SelectNodes("/job/printObjects/printObject/printLines/printLine");

            foreach (XmlNode line in printLines)
            {
                if (line.InnerXml.Contains("fixedText")) continue;

                if (line.InnerXml.Contains("GTIN")) 
                { 
                    line.RemoveAll();
                    line.InnerXml = "<fixedText>04605310011236</fixedText>";
                }

                if (line.InnerXml.Contains("Serial"))
                {
                    line.RemoveAll();
                    line.InnerXml = "<fixedText>0123456789123</fixedText>";
                }

                if (line.InnerXml.Contains("Lot"))
                {
                    line.RemoveAll();
                    line.InnerXml = "<fixedText>101022</fixedText>";
                }
                
                if (line.InnerXml.Contains("Eval"))
                {
                    line.RemoveAll();
                    line.InnerXml = "<fixedText>10/2025</fixedText>";
                }
            }

            xDoc.Save(xmlPath+"2");
        }


        private string[] LoadSgtins(string path)
        {
            var result = File.ReadAllLines(path);
            for(int i=0; i < result.Length; i++ ) result[i] = result[i].Trim('\r', '\n');
            if (result.Length == 0) throw new Exception("SGTIN не найдены!");
            return result;
        }

    }
}
