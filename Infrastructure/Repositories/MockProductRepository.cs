using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

// samma gränssnitt som ProductRepository men jobbar mot listan i MockData istället för databasen
public class MockProductRepository : IProductRepository
{
    public Task<IReadOnlyList<Product>> ListAllAsync()
    {
        IReadOnlyList<Product> list = MockData.Products.OrderBy(p => p.ItemNumber).ToList();
        return Task.FromResult(list);
    }

    public Task<Product?> FindByIdAsync(int id)
    {
        return Task.FromResult(MockData.Products.FirstOrDefault(p => p.Id == id));
    }
}
