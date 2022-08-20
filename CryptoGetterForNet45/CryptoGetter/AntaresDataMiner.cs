using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CryptoGetter
{
    internal class AntaresDataMiner : IDataMiner
    {
        //Соединение с БД
        private SqlConnection _connection;
        private readonly Server  _server;

        public AntaresDataMiner(Server server)
        {
            _server = server;
        }

        public (string, string) GetCrypto(string sGTIN)
        {
            string gtin = sGTIN.Substring(0,14);
            if (gtin.Length != 14) throw new ArgumentException("Неверная длина GTIN!");
            
            string serial = sGTIN.Substring(14);
            if (serial.Length != 13) throw new ArgumentException("Неверная длина серийного номера!");

            Connect(_server);

            //Получаем по GTIN его идентификатор.
            string gtinId = GetGtinId(gtin);

            //Проверяем найден ли GTIN
            if (gtinId.Length != 4)
            {
                throw new Exception("GTIN не найден!");
            }

            // По идентификатору GTIN и серийному номеру пачки получаем крипто-данные.
            return GetCryptoData(gtinId, serial);
        }

        public void Close()
        {
            Disconnect();
        }

        private void Connect(Server server)
        {
            Disconnect();
            string connectionString = $"Data Source={server.FQN};Initial Catalog={ server.DBName};Persist Security Info=True;" +
                $"User ID=tav;Password=tav";
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public void Disconnect()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        private string GetGtinId(string gtin)
        {
            string result = "";

            string cmdString = $"SELECT [Id] FROM {_server.DBName}.[dbo].[NtinDefinition] WHERE Ntin = '{gtin}'";
            SqlCommand cmd = new SqlCommand(cmdString, _connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result = reader.GetValue(0).ToString();
            }

            reader.Close();
            cmd.Dispose();

            return result;
        }

        private (string,string) GetCryptoData(string gtinId, string serial)
        {
            string cmdString = $"SELECT [VariableName] ,[VariableValue] FROM {_server.DBName}.[dbo].[ItemDetails] " +
                $"where Serial='{serial}' and NtinId={gtinId}";
            SqlCommand cmd = new SqlCommand(cmdString, _connection);
            cmd.CommandTimeout = 20;
            SqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, string> results = new Dictionary<string, string>();
            //Читаем по порядку все ответы
            while (reader.Read())
            {
                string key = reader.GetValue(0).ToString();
                string value = reader.GetValue(1).ToString();
                results.Add(key, value);
            }
            reader.Close();
            cmd.Dispose();

            string cryptoCode, cryptoKey;
            if (results.Count >= 2)
            {
                cryptoCode = results["cryptocode"];
                cryptoKey = results["cryptokey"];
            }
            else
            {
                throw new Exception("Криптоданные не найдены");
            }
            return (cryptoCode, cryptoKey);
        }
    }
}
