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

		public async Task<DataMinerResult> GetCodeAsync(string sgtin)
		{
			// 1. Валидация входных данных
			if (string.IsNullOrWhiteSpace(sgtin) || sgtin.Length != 27)
			{
				return DataMinerResult.Fail(DataMinerError.InvalidInput);
			}

			string gtin = sgtin[..14];
			string serial = sgtin[14..];

			// Medtech хранит код в GS1-формате
			string gs1Code = $"01{gtin}21{serial}";

			string commandString =
				"SELECT c.gs1field91, c.gs1field92 " +
				"FROM mark_un_code_gs1 AS c " +
				"JOIN mark_un_code AS g ON c.unid = g.unid " +
				"WHERE g.check_bar_code = @Gs1Code";

			var csb = new FbConnectionStringBuilder
			{
				Database = $"{_server.FQN}:{_server.DBName}",
				UserID = "FS_ADMIN",
				Password = "NdVj4K?9",
				Charset = "UTF8",
				Role = "RDB$ADMIN",
				ConnectionTimeout = 20
			};

			string cryptoKey = string.Empty;
			string cryptoCode = string.Empty;

			try
			{
				using var connection = new FbConnection(csb.ToString());
				await connection.OpenAsync();

				using var cmd = new FbCommand(commandString, connection)
				{
					CommandType = CommandType.Text
				};
				cmd.Parameters.AddWithValue("@Gs1Code", gs1Code);

				using var reader = await cmd.ExecuteReaderAsync();
				if (await reader.ReadAsync())
				{
					cryptoKey = reader.GetString(0);
					cryptoCode = reader.GetString(1);
				}
			}
			catch (FbException)
			{
				// Ошибка подключения / выполнения запроса
				return DataMinerResult.Fail(DataMinerError.SourceUnavailable);
			}

			// 2. Проверка результата
			if (string.IsNullOrEmpty(cryptoKey) || string.IsNullOrEmpty(cryptoCode))
			{
				return DataMinerResult.Fail(DataMinerError.NotFound);
			}

			// 3. Успех
			return DataMinerResult.Success($"{cryptoKey}:{cryptoCode}");
		}
	}
}
