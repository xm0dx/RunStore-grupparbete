using Core.Entities;

namespace Core.Interfaces;

// gränssnittet ligger i Core så att andra projekt kan använda det
public interface IProductRepository
{
    Task<IReadOnlyList<Product>> ListAllAsync();
    Task<Product?> FindByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}
