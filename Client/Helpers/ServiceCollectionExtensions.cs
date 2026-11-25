using System;
using Client.Enums;
using Client.Factories;
using Client.ViewModels;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;

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
        collection.AddTransient<CustomerViewModel>();
        collection.AddTransient<SalesOrdersViewModel>();
        // vi kör mot testdata i minnet (MockData), ingen databas behövs
        // byt till ProductRepository / SupplierRepository för att köra mot Neon
        collection.AddScoped<IProductRepository, MockProductRepository>();
        collection.AddScoped<ISupplierRepository, MockSupplierRepository>();
        collection.AddScoped<ICustomerRepository, MockCustomerRepository>();
        collection.AddScoped<ISalesOrderRepository, MockSalesOrderRepository>();

        collection.AddSingleton<Func<PageViewsEnum, ViewModelBase>>(c =>
            name =>
                name switch
                {
                    PageViewsEnum.Home => c.GetRequiredService<HomeViewModel>(),
                    PageViewsEnum.Products => c.GetRequiredService<ProductsViewModel>(),
                    PageViewsEnum.Suppliers => c.GetRequiredService<SuppliersViewModel>(),
            PageViewsEnum.Customers => c.GetRequiredService<CustomerViewModel>(),
                    PageViewsEnum.SalesOrder => c.GetRequiredService<SalesOrdersViewModel>(),
                    _ => throw new InvalidOperationException(),
                }
        );
    }
}
