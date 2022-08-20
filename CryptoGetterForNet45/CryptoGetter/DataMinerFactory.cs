using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGetter
{
    internal class DataMinerFactory
    {
        public IDataMiner GetDataMiner(ServerList.ServerType typeOfServer)
        {
            
            if (typeOfServer == ServerList.ServerType.Antares) return new AntaresDataMiner();
            if (typeOfServer == ServerList.ServerType.Medtech) return new MedtechDataMiner();
            throw new Exception ("Класс типа сервера не описан!");
        }

    }
}
