using Microsoft.Data.SqlClient;

namespace CryptogetterBlazorApp.CryptoGetter
{
	internal class AntaresDataMiner : IDataMiner
	{
		private readonly Server _server;

		public AntaresDataMiner(Server server)
		{
			_server = server;
		}

		public async Task<DataMinerResult> GetCodeAsync(string sgtin)
		{
			// 1. Валидация входных данных (бизнес-ошибка, не exception)
			if (string.IsNullOrWhiteSpace(sgtin) || sgtin.Length != 27)
			{
				return DataMinerResult.Fail(DataMinerError.InvalidInput);
			}

			string gtin = sgtin[..14];
			string serial = sgtin[14..];

			string connectionString =
				$"Data Source={_server.FQN};" +
				$"Initial Catalog={_server.DBName};" +
				$"Persist Security Info=True;" +
				$"User ID=tav;Password=tav;" +
				$"TrustServerCertificate=True";

			string cmdString =
				$"SELECT i.VariableName, i.VariableValue " +
				$"FROM [{_server.DBName}].[dbo].[ItemDetails] AS i " +
				$"JOIN [{_server.DBName}].[dbo].[NtinDefinition] AS n ON i.NtinId = n.Id " +
				$"WHERE i.Serial = @Serial AND n.Ntin = @Ntin";

			var results = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			try
			{
				using var connection = new SqlConnection(connectionString);
				await connection.OpenAsync();

				using var cmd = new SqlCommand(cmdString, connection);
				cmd.Parameters.AddWithValue("@Serial", serial);
				cmd.Parameters.AddWithValue("@Ntin", gtin);

				using var reader = await cmd.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					var key = reader.GetString(0);
					var value = reader.GetString(1);

					results[key] = value;
				}
			}
			catch (SqlException)
			{
				// проблема с БД / сетью — инфраструктурная ошибка
				return DataMinerResult.Fail(DataMinerError.SourceUnavailable);
			}

			// 2. Проверка бизнес-результата
			if (!results.TryGetValue("cryptokey", out var cryptoKey) ||
				!results.TryGetValue("cryptocode", out var cryptoCode))
			{
				// данных нет — штатная бизнес-ситуация
				return DataMinerResult.Fail(DataMinerError.NotFound);
			}

			// 3. Успех
			return DataMinerResult.Success($"{cryptoKey}:{cryptoCode}");
		}
	}
}
