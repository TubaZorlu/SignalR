using Microsoft.EntityFrameworkCore;

namespace UPSchool_SignalR_Api2.Models
{
	public class Context:DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
		}

		public DbSet<Elektrik> Elektriks { get; set; }
	}
}
