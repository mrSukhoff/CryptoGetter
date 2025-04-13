using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace CryptogetterBlazorApp.CryptoGetter
{
	internal class MedtechDataMiner : IDataMiner
	{
		private readonly Server _server;

		public MedtechDataMiner(Server server)
		{
			_server = server;
		}

		public async Task<(string, string)> GetCrypto(string sGTIN)
		{
			if (sGTIN.Length != 27) throw new ArgumentException("Неверная длина SGTIN!");

			string gtin = sGTIN[..14];
			string serial = sGTIN[14..];

			sGTIN = "01" + gtin + "21" + serial;

			string commanString = $"SELECT c.gs1field91, c.gs1field92 FROM mark_un_code_gs1 AS c JOIN mark_un_code AS g " +
				$"ON c.unid = g.unid WHERE g.check_bar_code = '{sGTIN}'";

			FbConnectionStringBuilder connectionStringBuilder = new()
			{
				Database = $"{_server.FQN}:{_server.DBName}",
				UserID = "FS_ADMIN",
				Password = "NdVj4K?9",
				Charset = "UTF8",
				Role = "RDB$ADMIN",
				ConnectionTimeout = 20
			};

			string connectionString = connectionStringBuilder.ToString();

			string cryptoKey = "", cryptoCode = "";

			using (var connection = new FbConnection(connectionString))
			{
				await connection.OpenAsync(); // Асинхронное открытие соединения
				using (FbCommand cmd = new FbCommand(commanString, connection) { CommandType = CommandType.Text })
				{
					using (FbDataReader reader = await cmd.ExecuteReaderAsync()) // Асинхронное выполнение запроса
					{
						while (await reader.ReadAsync()) // Асинхронный перебор строк
						{
							cryptoKey = reader.GetString(0);
							cryptoCode = reader.GetString(1);
						}
					}
				}
			}

			if (string.IsNullOrEmpty(cryptoKey) || string.IsNullOrEmpty(cryptoCode))
				throw new Exception($"Криптоданные для КИЗ {sGTIN} не найдены в базе {_server.DBName}");

			return (cryptoKey, cryptoCode);
		}
	}
}