using System;
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
            if (sGTIN.Length != 27) throw new ArgumentException("Неверная длина SGTIN!");

            string gtin = sGTIN.Substring(0, 14);
            string serial = sGTIN.Substring(14);

            sGTIN = "01" + gtin + "21" + serial;

            string commanString = $"select c.gs1field91, c.gs1field92 from mark_un_code_gs1 as c join mark_un_code as g " +
                $"on c.unid = g.unid where g.check_bar_code = '{sGTIN}'";
           
            FbConnectionStringBuilder connectionStringBuilder = new FbConnectionStringBuilder
            {
                Database = $"{_server.FQN}:{_server.DBName}",
                UserID = "FS_ADMIN",
                Password = "NdVj4K?9",
                Charset = "UTF8",
                Role = "RDB$ADMIN",
            };
            
            string connectionString = connectionStringBuilder.ToString();

            string cryptoKey = "",  cryptoCode = "";

            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (FbCommand cmd = new FbCommand(commanString, connection) { CommandType = CommandType.Text })
                {
                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cryptoKey = reader.GetString(0);
                            cryptoCode = reader.GetString(1);
                        }
                    }
                }
            }

            if (cryptoKey == "" || cryptoCode == "") throw new Exception("Криптоданные не найдены");
            return (cryptoKey, cryptoCode);
        }

    }
}
