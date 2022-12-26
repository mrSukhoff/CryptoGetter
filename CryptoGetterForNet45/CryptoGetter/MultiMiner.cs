using CryptoGetter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            var sgtins = LoadSgtins(sgtinPath);

        }


    private List<string> LoadSgtins(string path)
    {
        var result = new List<string>();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim('\r', '\n');
                if (line.Length != 27) throw new Exception("Неверная длина SGTIN");
                result.Add(line);
            }
        }
        if (result.Count == 0) throw new Exception("SGTIN не найдены!");
        return result;
    }

        private XmlDocument LoadXml(string path) 
        {
            var result = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim('\r', '\n');
                    if (line.Length != 27) throw new Exception("Неверная длина SGTIN");
                    result.Add(line);
                }
            }
            if (result.Count == 0) throw new Exception("SGTIN не найдены!");
            return result;
        }

    }
}
