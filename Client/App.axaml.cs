using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Helpers;
using Client.ViewModels;
using Client.Views;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddServices();

        // anslutningssträngen ligger i user-secrets istället för i en fil i projektet
        // dotnet user-secrets set ConnectionStrings:pgsql "..."
        var builder = Host.CreateApplicationBuilder();
        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

        var connectionString = builder.Configuration.GetConnectionString("pgsql");

        // dbcontext registreras här så vi kan skicka med anslutningssträngen
        // används inte så länge mock-repositories är registrerade i ServiceCollectionExtensions
        collection.AddDbContext<RunStoreDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            // skriver ut sql i terminalen så vi ser vad ef core gör
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });

        var serviceProvider = collection.BuildServiceProvider();
        var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();

        // kör migreringarna och lägger in testdata vid start, avstängt tills vidare
        var context = serviceProvider.GetRequiredService<RunStoreDbContext>();

        try
        {
            context.Database.Migrate();
            SeedData.AddData(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
