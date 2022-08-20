using System;
using System.Collections.Generic;
using System.IO;


namespace CryptoGetter
{
    //Формат списка серверов
    public class ServerList
    {
        public List<Server> ListOfServers;

        public ServerList()
        {
            string path = @"server.ini";
            ListOfServers = new List<Server>();

            if (File.Exists(path))
            {
                List<string> lines = new List<string>();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] word = line.Split(' ');
                        string name = word[0];
                        string fqn = word[1];
                        string dbname = word[2];
                        
                        ServerType serverType;
                        switch (word[3])
                        {
                            case /*ServerType.Antares.ToString()*/"Antares":
                                serverType = ServerType.Antares;
                                break;

                            case /*ServerType.Medtech.ToString()*/"Medtech":
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
