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
            ServerNames = [];
            foreach (Server server in _listOfServers) ServerNames.Add(server.Name);
        }

        private void GetServerList()
        {
            string path = "server.ini";

            _listOfServers = [];

            if (File.Exists(path))
            {
                List<string> lines = [];
                using (StreamReader sr = new(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] word = line.Split(',', ';');
                        string name = word[0];
                        string fqn = word[1];
                        string dbname = word[2];
                        var serverType = word[3] switch
                        {
                            "Antares" => ServerType.Antares,
                            "Medtech" => ServerType.Medtech,
                            _ => throw new ArgumentException("Неверный формат типа сервера в файле server.ini"),
                        };
                        Server server = new()
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
