using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace CryptogetterBlazorApp.CryptoGetter
{
	internal class AntaresDataMiner : IDataMiner
	{
		private readonly Server _server;

		public AntaresDataMiner(Server server)
		{
			_server = server;
		}

		public async Task<(string, string)> GetCrypto(string sGTIN)
		{
			if (sGTIN.Length != 27) throw new ArgumentException("Неверная длина SGTIN!");

			string gtin = sGTIN[..14];
			string serial = sGTIN[14..];

			string connectionString = $"Data Source={_server.FQN};Initial Catalog={_server.DBName};Persist Security Info=True;" +
				$"User ID=tav;Password=tav;TrustServerCertificate=True";

			string cmdString = $"SELECT i.VariableName, i.VariableValue FROM [{_server.DBName}].[dbo].[ItemDetails] AS i " +
				$"JOIN [{_server.DBName}].[dbo].[NtinDefinition] AS n ON i.NtinId = n.Id " +
				$"WHERE i.Serial = @Serial AND n.Ntin = @Ntin";

			Dictionary<string, string> results = [];

			using (var connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync(); // Асинхронное открытие соединения
				using (SqlCommand cmd = new(cmdString, connection))
				{
					cmd.Parameters.AddWithValue("@Serial", serial);
					cmd.Parameters.AddWithValue("@Ntin", gtin);
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) // Асинхронное чтение
					{
						while (await reader.ReadAsync()) // Асинхронный перебор строк
						{
							string key = reader.GetValue(0)?.ToString() ?? throw new Exception("Поле VariableName не может быть NULL");
							string value = reader.GetValue(1)?.ToString() ?? throw new Exception("Поле VariableValue не может быть NULL");
							if (results.ContainsKey(key))
								results[key] = value;
							else
								results.Add(key, value);
						}
					}
				}
			}

			string cryptoCode, cryptoKey;
			if (results.Count >= 2 && results.ContainsKey("cryptokey") && results.ContainsKey("cryptocode"))
			{
				cryptoKey = results["cryptokey"];
				cryptoCode = results["cryptocode"];
			}
			else
			{
				throw new Exception($"Криптоданные для КИЗ {sGTIN} не найдены в базе {_server.DBName}");
			}
			return (cryptoKey, cryptoCode);
		}
	}
}