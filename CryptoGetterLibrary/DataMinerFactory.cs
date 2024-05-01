using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoGetterLibrary
{
    public class DataMinerFactory
    {
        public List<string> ServerNames { get; }
        private List<Server> _listOfServers;

        public IDataMiner GetDataMiner(string serverName)
        {
            Server server = _listOfServers.First(s => s.Name == serverName);
            if (server.Type == ServerType.Antares) return new AntaresDataMiner(server);
            if (server.Type == ServerType.Medtech) return new MedtechDataMiner(server);
            throw new Exception("Класс типа сервера не описан!");
        }

        public DataMinerFactory()
        {
            GetServerList();
            ServerNames = new List<string>();
			foreach (Server server in _listOfServers) ServerNames.Add(server.Name);
        }

        private void GetServerList()
        {
            string path = "server.ini";

            _listOfServers = new List<Server>();

            if (File.Exists(path))
            {
                List<string> lines = new List<string>();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] word = line.Split(',', ';');
                        string name = word[0];
                        string fqn = word[1];
                        string dbname = word[2];

                        ServerType serverType;
                        switch (word[3])
                        {
                            case "Antares":
                                serverType = ServerType.Antares;
                                break;

                            case "Medtech":
                                serverType = ServerType.Medtech;
                                break;
                            default: throw new ArgumentException("Неверный формат типа сервера в файле server.ini");
                        }

                        Server server = new Server
                        {
                            Name = name,
                            FQN = fqn,
                            DBName = dbname,
                            Type = serverType
                        };
                        _listOfServers.Add(server);
                    }
                }
            }
            else
            // Если ничего не нашли создаём сервер по умолчанию
            {
                _listOfServers.Add(new Server { Name = "Иркутск_ТСТ", FQN = "irk-sql-tst", DBName = "AntaresTracking_QA", Type = ServerType.Antares });
            }
        }
    }
}
