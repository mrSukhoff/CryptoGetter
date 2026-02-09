namespace CryptogetterBlazorApp.CryptoGetter
{
	public enum CodeGenerationError
	{
		Unauthorized,
		NoCodesAvailable,
		UnknownPrefix,
		SourceUnavailable,
		InvalidInput,
		InternalError
	}

	public sealed class CodeGenerationResult
	{
		public bool IsSuccess { get; }
		public string? Code { get; }
		public CodeGenerationError? Error { get; }

		// новое
		public string? ServerName { get; }

		private CodeGenerationResult(
			bool isSuccess,
			string? code,
			CodeGenerationError? error,
			string? serverName)
		{
			IsSuccess = isSuccess;
			Code = code;
			Error = error;
			ServerName = serverName;
		}

		public static CodeGenerationResult Success(string code, string serverName)
		{
			return new CodeGenerationResult(
				isSuccess: true,
				code: code,
				error: null,
				serverName: serverName);
		}

		public static CodeGenerationResult Fail(CodeGenerationError error)
		{
			return new CodeGenerationResult(
				isSuccess: false,
				code: null,
				error: error,
				serverName: null);
		}
	}


}
