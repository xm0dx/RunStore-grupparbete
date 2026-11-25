using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

// som ProductRepository men mot listan i MockData
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

    public Task<bool> AddAsync(Product product)
    {
        try
        {
            product.Id = MockData.Products.Count + 1;
            MockData.Products.Add(product);
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> UpdateAsync(Product product)
    {
        var productFound = MockData.Products.Find(p => p.Id == product.Id);

        if (productFound != null)
        {
            productFound.ImageUrl = product.ImageUrl;
            productFound.ItemNumber = product.ItemNumber;
            productFound.Name = product.Name;
            productFound.Price = product.Price;
            productFound.Supplier = product.Supplier;
            productFound.SupplierId = product.SupplierId;
            return Task.FromResult(true);
        }
        else
        {
            return Task.FromResult(false);
        }
    }
}
