namespace CryptogetterBlazorApp.CryptoGetter
{
    //Формат списка серверов
    public class ServerList
    {
		private List<Server> listOfServers;

		public List<Server> ListOfServers { get => listOfServers;}

		public ServerList()
        {
			string path = Path.Combine(Directory.GetCurrentDirectory(), "server.ini");

			listOfServers = [];

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
                        listOfServers.Add(server);
                    }
                }
            }
            else
            // Если ничего не нашли создаём сервер по умолчанию
            {
                listOfServers.Add(new Server { Name = "Иркутск_ТСТ", FQN = "irk-sql-tst", DBName = "AntaresTracking_QA", Type = ServerType.Antares });
            }
        }
	}
}
