using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PortKisel.Context
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<PortContext>
    {
        public PortContext CreateDbContext(string[] options)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<PortContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new PortContext(options);
        }
    }
}
