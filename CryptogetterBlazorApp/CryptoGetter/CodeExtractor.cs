namespace CryptogetterBlazorApp.CryptoGetter
{
	public class CodeExtractor
	{
		private ServerList _serverList;
		private DataMinerFactory _dataMiner;

		public CodeExtractor(ServerList serverList)
		{
			_serverList = serverList;
			_dataMiner = new DataMinerFactory();
		}

		public async Task<string> ExtractCode (string kiz)
		{
			Server server = DefineServer(kiz);
			var dataMiner = _dataMiner.GetDataMiner(server);
			var (cryptoKey, cryptoCode) = await dataMiner.GetCrypto(kiz);
			return $"01{kiz[..14]}21{kiz[14..]}{char.ConvertFromUtf32(29)}91{cryptoKey}{char.ConvertFromUtf32(29)}92{cryptoCode}";
		}

		private Server DefineServer(string kiz)
		{
			// Извлекаем префикс GS1 (первые 8 символов GTIN)
			string gs1Prefix = kiz.Substring(0, 8);

			// Извлекаем серийный номер (последние 13 символов)
			string serialNumber = kiz.Substring(14);

			// Проверяем, содержит ли серийный номер буквы
			bool hasLetters = serialNumber.Any(char.IsLetter);

			// Определяем тип сервера: если есть буквы — Medtech, если только цифры — Антарес
			ServerType serverType = hasLetters ? ServerType.Medtech : ServerType.Antares;

			// Ищем сервер с подходящим префиксом и типом
			var server = _serverList.ListOfServers.FirstOrDefault(s =>
				s.GS1Prefix == gs1Prefix && s.Type == serverType);

			if (server == null)
			{
				throw new InvalidOperationException($"Сервер для префикса {gs1Prefix} и типа {serverType} не найден.");
			}

			return server;
		}
	}
}
