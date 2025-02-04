using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RDR2.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Get the path to the Api project directory (where appsettings.json is located)
            var apiProjectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "../RDR2.Api");

            // Build the configuration to access the connection string
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectDirectory) // Set the base path to RDR2.Api directory
                .AddJsonFile("appsettings.json")  // Specify the appsettings.json file in RDR2.Api
                .Build();

            // Use the connection string from the appsettings.json file
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
