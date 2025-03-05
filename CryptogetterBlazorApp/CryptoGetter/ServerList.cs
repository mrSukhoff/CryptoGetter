namespace CryptogetterBlazorApp.CryptoGetter
{
    //Формат списка серверов
    public class ServerList
    {
        public List<Server> ListOfServers;

        public ServerList()
        {
			string path = Path.Combine(Directory.GetCurrentDirectory(), "server.ini");

			ListOfServers = [];

            if (File.Exists(path))
            {
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
						Server server = new Server
                        {
                            Name = name,
                            FQN = fqn,
                            DBName = dbname,
                            Type = serverType
                        };
                        ListOfServers.Add(server);
                    }
                }
            }
            else
            // Если ничего не нашли создаём сервер по умолчанию
            {
                ListOfServers.Add(new Server { Name = "Иркутск_ТСТ", FQN = "irk-sql-tst", DBName = "AntaresTracking_QA", Type = ServerType.Antares });
            }
        }
    }


}
