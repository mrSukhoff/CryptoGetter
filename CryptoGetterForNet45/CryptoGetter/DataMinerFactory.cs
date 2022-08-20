using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGetter
{
    internal class DataMinerFactory
    {
        public IDataMiner GetDataMiner(Server server)
        {
            
            if (server.Type == ServerType.Antares) return new AntaresDataMiner(server);
            if (server.Type == ServerType.Medtech) return new MedtechDataMiner(server);
            throw new Exception ("Класс типа сервера не описан!");
        }

    }
}
