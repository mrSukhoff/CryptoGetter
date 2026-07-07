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

	public async Task<CodeGenerationResult> GenerateAsync(string ic)
	{
		// 1. Валидация
		if (string.IsNullOrWhiteSpace(ic) || ic.Length != 27)
		{
			return CodeGenerationResult.Fail(ic, CodeGenerationError.InvalidInput);
		}

		// 2. Определяем сервер
		Server? server = DefineServer(ic);
		if (server == null)
		{
			return CodeGenerationResult.Fail(ic, CodeGenerationError.UnknownPrefix);
		}

		// 3. Получаем DataMiner
		IDataMiner? miner = _factory.TryGetDataMiner(server);
		if (miner == null)
		{
			return CodeGenerationResult.Fail(ic, CodeGenerationError.UnknownPrefix);
		}

		// 4. Запрашиваем код
		DataMinerResult minerResult = await miner.GetCodeAsync(ic);
		if (!minerResult.IsSuccess)
		{
			return MapMinerError(ic, minerResult.Error);
		}

		// 5. Формируем GS1
		string gs1 = BuildGs1(ic, minerResult.Code!);

		return CodeGenerationResult.Success(ic, gs1, server.Name);
	}

	public async Task<List<CodeGenerationResult>> GenerateMultipleAsync(string[] iCs)
	{
		var results = new List<CodeGenerationResult>();

		if (iCs == null || iCs.Length == 0)
		{
			results.Add(CodeGenerationResult.Fail("all", CodeGenerationError.InvalidInput));
			return results;
		}

		foreach (var ic in iCs)
		{
			if (string.IsNullOrWhiteSpace(ic))
			{
				results.Add(CodeGenerationResult.Fail(ic, CodeGenerationError.InvalidInput));
				continue;
			}

			try
			{
				var result = await GenerateAsync(ic);
				results.Add(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while processing OriginalIC: {OriginalIC}", ic);
				results.Add(CodeGenerationResult.Fail(ic, CodeGenerationError.InternalError));
			}
		}

		return results;
	}


	// ---------------- private helpers ----------------

	private Server? DefineServer(string ic)
	{
		string gs1Prefix = ic[..8];
		string serial = ic[14..];
		bool hasLetters = serial.Any(char.IsLetter);

		var serverType = hasLetters
			? ServerType.Medtech
			: ServerType.Antares;

		return _serverList.ListOfServers.FirstOrDefault(s =>
			s.GS1Prefix == gs1Prefix && s.Type == serverType);
	}

	private static CodeGenerationResult MapMinerError(string ic, DataMinerError? error)
	{
		return error switch
		{
			DataMinerError.NotFound =>
				CodeGenerationResult.Fail(ic, CodeGenerationError.NoCodesAvailable),

			DataMinerError.SourceUnavailable =>
				CodeGenerationResult.Fail(ic, CodeGenerationError.SourceUnavailable),

			DataMinerError.InvalidInput =>
				CodeGenerationResult.Fail(ic, CodeGenerationError.InvalidInput),

			_ =>
				CodeGenerationResult.Fail(ic, CodeGenerationError.InternalError)
		};
	}

	private static string BuildGs1(string ic, string cryptoData)
	{
		// cryptoData = "key:code"
		var parts = cryptoData.Split(':', 2);

		string gtin = ic[..14];
		string serial = ic[14..];
		string gs = char.ConvertFromUtf32(29);

		return $"01{gtin}21{serial}{gs}91{parts[0]}{gs}92{parts[1]}";
	}
}
