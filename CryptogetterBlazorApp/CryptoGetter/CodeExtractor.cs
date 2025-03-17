namespace CryptogetterBlazorApp.CryptoGetter
{
	public class CodeExtractor
	{
		private readonly ServerList _serverList;
		private readonly DataMinerFactory _dataMinerFactory;

		public CodeExtractor(ServerList serverList)
		{
			_serverList = serverList;
			_dataMinerFactory = new DataMinerFactory();
		}

		public async Task<(string,string)> ExtractCode (string kiz)
		{
			Server server = DefineServer(kiz);
			var dataMiner = _dataMinerFactory.GetDataMiner(server);
			var (cryptoKey, cryptoCode) = await dataMiner.GetCrypto(kiz);
			return ($"01{kiz[..14]}21{kiz[14..]}{char.ConvertFromUtf32(29)}91{cryptoKey}{char.ConvertFromUtf32(29)}92{cryptoCode}", server.Name);
		}

		private Server DefineServer(string kiz)
		{
			// Извлекаем префикс GS1 (первые 8 символов GTIN)
			string gs1Prefix = kiz[..8];

			// Извлекаем серийный номер (последние 13 символов)
			string serialNumber = kiz[14..];

			// Проверяем, содержит ли серийный номер буквы
			bool hasLetters = serialNumber.Any(char.IsLetter);

			// Определяем тип сервера: если есть буквы — Medtech, если только цифры — Антарес
			ServerType serverType = hasLetters ? ServerType.Medtech : ServerType.Antares;

			// Ищем сервер с подходящим префиксом и типом
			var server = _serverList.ListOfServers.FirstOrDefault(s =>
				s.GS1Prefix == gs1Prefix && s.Type == serverType);

			return server ?? throw new InvalidOperationException($"Сервер для префикса {gs1Prefix} и типа {serverType} не найден.");
		}
	}
}
