using CryptogetterBlazorApp.CryptoGetter;

public class CodeGenerationService
{
	private readonly ServerList _serverList;
	private readonly ILogger<CodeGenerationService> _logger;
	private readonly DataMinerFactory _factory;

	public CodeGenerationService(ServerList serverList, ILogger<CodeGenerationService> logger)
	{
		_serverList = serverList;
		_logger = logger;
		_factory = new();
	}

	public async Task<CodeGenerationResult> GenerateAsync(string kiz)
	{
		// 1. Валидация
		if (string.IsNullOrWhiteSpace(kiz) || kiz.Length != 27)
		{
			return CodeGenerationResult.Fail(CodeGenerationError.InvalidInput);
		}

		// 2. Определяем сервер
		var server = DefineServer(kiz);
		if (server == null)
		{
			return CodeGenerationResult.Fail(CodeGenerationError.UnknownPrefix);
		}

		// 3. Получаем DataMiner
		var miner = _factory.TryGetDataMiner(server);
		if (miner == null)
		{
			return CodeGenerationResult.Fail(CodeGenerationError.UnknownPrefix);
		}

		// 4. Запрашиваем код
		var minerResult = await miner.GetCodeAsync(kiz);
		if (!minerResult.IsSuccess)
		{
			return MapMinerError(minerResult.Error);
		}

		// 5. Формируем GS1
		string gs1 = BuildGs1(kiz, minerResult.Code!);

		return CodeGenerationResult.Success(gs1,server.Name);
	}

	// ---------------- private helpers ----------------

	private Server? DefineServer(string kiz)
	{
		string gs1Prefix = kiz[..8];
		string serial = kiz[14..];
		bool hasLetters = serial.Any(char.IsLetter);

		var serverType = hasLetters
			? ServerType.Medtech
			: ServerType.Antares;

		return _serverList.ListOfServers.FirstOrDefault(s =>
			s.GS1Prefix == gs1Prefix && s.Type == serverType);
	}

	private static CodeGenerationResult MapMinerError(DataMinerError? error)
	{
		return error switch
		{
			DataMinerError.NotFound =>
				CodeGenerationResult.Fail(CodeGenerationError.NoCodesAvailable),

			DataMinerError.SourceUnavailable =>
				CodeGenerationResult.Fail(CodeGenerationError.SourceUnavailable),

			DataMinerError.InvalidInput =>
				CodeGenerationResult.Fail(CodeGenerationError.InvalidInput),

			_ =>
				CodeGenerationResult.Fail(CodeGenerationError.InternalError)
		};
	}

	private static string BuildGs1(string kiz, string cryptoData)
	{
		// cryptoData = "key:code"
		var parts = cryptoData.Split(':', 2);

		string gtin = kiz[..14];
		string serial = kiz[14..];
		string gs = char.ConvertFromUtf32(29);

		return $"01{gtin}21{serial}{gs}91{parts[0]}{gs}92{parts[1]}";
	}
}
