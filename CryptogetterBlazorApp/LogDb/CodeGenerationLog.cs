namespace CryptogetterBlazorApp.LogDb
{
	public class CodeGenerationLog
	{
		public int Id { get; set; }
		public string? UserName { get; set; } // Nullable
		public DateTime RequestDateTime { get; set; }
		public string? Kiz { get; set; } // Nullable
		public string? Recipient { get; set; } // Nullable
	}
}