using System;
using System.Collections.Generic;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace CryptoGetter
{
    internal class MedtechDataMiner : IDataMiner
    {
        private readonly Server _server;

        public MedtechDataMiner(Server server)
        {
            _server = server;
        }

        public (string, string) GetCrypto(string sGTIN)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            string commanString = $"select c.gs1field91, c.gs1field92 from mark_un_code_gs1 as c join mark_un_code as g " +
                $"on c.unid = g.unid where g.check_bar_code = '{sGTIN}'";

            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder
            {
                Database = $"{_server.FQN}:{_server.DBName}",
                UserID = "FS_ADMIN",
                Password = "NdVj4K?9",
                Charset = "UTF8",
                Role = "USER_ROLE",
            };
            string connectionString = connectionStringBuilder.ToString();

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (FbCommand cmd = new FbCommand(commanString, connection) { CommandType = CommandType.Text })
                {
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader.GetValue(0).ToString();
                            string value = reader.GetValue(1).ToString();
                            results.Add(key, value);
                        }
                    }
                }
            }
        
            string cryptoCode, cryptoKey;
            if (results.Count >= 2)
            {
                cryptoKey = results["cryptokey"];
                cryptoCode = results["cryptocode"];
            }
            else
            {
                throw new Exception("Криптоданные не найдены");
            }
            return (cryptoKey, cryptoCode);
        }

    }
}
