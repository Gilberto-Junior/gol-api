using Gol.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gol.Infrastructure
{
    public class GolContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GolContext()
        { }

        public GolContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Airplane> Airplanes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-ALJIDDC\SQLEXPRESS;Initial Catalog=GolDB;Integrated Security=True");
            var connectionString = _configuration.GetSection("ConnectionStrings:GolDB")?.Value;

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
