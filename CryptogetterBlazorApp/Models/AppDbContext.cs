using Microsoft.EntityFrameworkCore;
using CryptogetterBlazorApp.Models;

namespace CryptogetterBlazorApp.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<CodeGenerationLog> CodeGenerationLogs { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
	}
}