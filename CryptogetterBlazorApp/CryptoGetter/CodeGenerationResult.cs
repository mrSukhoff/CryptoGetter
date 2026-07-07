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
		public string OriginalIC { get; }
		public bool IsSuccess { get; }
		public string? Code { get; }
		public CodeGenerationError? Error { get; }
		public string? ServerName { get; }

		private CodeGenerationResult(
			string ic,
			bool isSuccess,
			string? code,
			CodeGenerationError? error,
			string? serverName)
		{
			OriginalIC = ic;
			IsSuccess = isSuccess;
			Code = code;
			Error = error;
			ServerName = serverName;
		}

		public static CodeGenerationResult Success(string ic, string code, string serverName)
		{
			return new CodeGenerationResult(
				ic:ic,
				isSuccess: true,
				code: code,
				error: null,
				serverName: serverName);
		}

		public static CodeGenerationResult Fail(string ic, CodeGenerationError error)
		{
			return new CodeGenerationResult(
				ic:ic,
				isSuccess: false,
				code: null,
				error: error,
				serverName: null);
		}
	}
}
