using Core.Entities;

namespace Core.Interfaces;

// gränssnittet ligger i Core så att andra projekt kan använda det
public interface IProductRepository
{
    Task<IReadOnlyList<Product>> ListAllAsync();
    Task<Product?> FindByIdAsync(int id);
    Task<bool> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
}
