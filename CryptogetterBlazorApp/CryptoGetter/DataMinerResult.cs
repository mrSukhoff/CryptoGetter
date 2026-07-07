namespace CryptogetterBlazorApp.CryptoGetter
{
	public sealed class DataMinerResult
	{
		public bool IsSuccess { get; init; }
		public string? Code { get; init; }
		public DataMinerError? Error { get; init; }

		public static DataMinerResult Success(string code) => new() { IsSuccess = true, Code = code };

		public static DataMinerResult Fail(DataMinerError error) => new() { IsSuccess = false, Error = error };
	}


	public enum DataMinerError
	{
		NotFound,           // кодов нет
		SourceUnavailable,  // БД недоступна
		InvalidInput        // битый sgtin
	}
}
