using System.Reflection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Client.Factories;

// används av dotnet ef när vi gör migreringar
// utan den försöker verktyget starta hela appen för att hitta dbcontext
public class DatabaseDesignTimeFactory : IDesignTimeDbContextFactory<RunStoreDbContext>
{
    public RunStoreDbContext CreateDbContext(string[] args)
    {
        // samma sak som i App.axaml.cs, anslutningssträngen från user-secrets
        var host = Host.CreateApplicationBuilder();
        host.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

        var connectionString = host.Configuration.GetConnectionString("pgsql");

        var builder = new DbContextOptionsBuilder<RunStoreDbContext>();
        builder.UseNpgsql(connectionString);

        return new RunStoreDbContext(builder.Options);
    }
}
