using Microsoft.EntityFrameworkCore;

namespace CryptogetterBlazorApp.LogDb
{
	public class AppDbContext : DbContext
	{
		public DbSet<CodeGenerationLog> CodeGenerationLogs { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
	}
}