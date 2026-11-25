using System;
using Client.Enums;
using Client.Factories;
using Core.Interfaces;
using Infrastructure.Repositories;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageViewFactory>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<ProductsViewModel>();
        collection.AddTransient<SuppliersViewModel>();
        collection.AddScoped<IProductRepository, ProductRepository>();
        collection.AddScoped<ISupplierRepository, SupplierRepository>();

        collection.AddSingleton<Func<PageViewsEnum, ViewModelBase>>(c => name => name switch
        {
            PageViewsEnum.Home => c.GetRequiredService<HomeViewModel>(),
            PageViewsEnum.Products => c.GetRequiredService<ProductsViewModel>(),
            PageViewsEnum.Suppliers => c.GetRequiredService<SuppliersViewModel>(),
            _ => throw new InvalidOperationException()
        });
    }
}
