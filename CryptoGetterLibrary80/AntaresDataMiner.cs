﻿using Microsoft.Data.SqlClient;

namespace CryptoGetterLibrary
{
    internal class AntaresDataMiner(Server server) : IDataMiner
    {
        private readonly Server _server = server;

        public (string, string) GetCrypto(string sGTIN)
        {
            if (sGTIN.Length != 27) throw new ArgumentException("Неверная длина SGTIN!");

            string gtin = sGTIN.Substring(0, 14);
            string serial = sGTIN.Substring(14);

            string connectionString = $"Data Source={_server.FQN};Initial Catalog={_server.DBName};Persist Security Info=True;" +
                "User ID=tav;Password=tav";

            string cmdString = "SELECT i.VariableName ,i.VariableValue FROM ItemDetails as i " +
                "JOIN NtinDefinition as n ON i.NtinId = n.Id " +
                $"WHERE i.Serial='{serial}' and n.Ntin='{gtin}'";

            Dictionary<string, string> results = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new(cmdString, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
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