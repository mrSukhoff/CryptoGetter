using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace CryptoGetter
{
    internal class MedtechDataMiner : IDataMiner
    {
        private readonly Server _server;
        private FbConnection _connection;

        public MedtechDataMiner(Server server)
        {
            _server = server;
        }

        public (string, string) GetCrypto(string sGTIN)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            Disconnect();
        }

        private void Connect() 
        {
            Disconnect();
            string connectionString = $"Data Source={_server.FQN};Initial Catalog={_server.DBName};Persist Security Info=True;" +
                $"User ID=tav;Password=tav";
            _connection = new FbConnection(connectionString);
            _connection.Open();
        }

        private void Disconnect()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }
    }
}
