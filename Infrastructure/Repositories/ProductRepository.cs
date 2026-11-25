using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

// repository pratar med databasen via dbcontext
public class ProductRepository(RunStoreDbContext context) : IProductRepository
{
    private readonly RunStoreDbContext _context = context;

    public async Task<IReadOnlyList<Product>> ListAllAsync()
    {
        // include tar med leverantören, annars är Supplier null
        return await _context.Products.Include(p => p.Supplier).ToListAsync();
    }

    public async Task<Product?> FindByIdAsync(int id)
    {
        // FindAsync letar på primärnyckeln, null om produkten inte finns
        return await _context.Products.FindAsync(id);
    }

    public async Task<bool> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var productFound = await FindByIdAsync(product.Id);

        if (productFound != null)
        {
            productFound.ImageUrl = product.ImageUrl;
            productFound.ItemNumber = product.ItemNumber;
            productFound.Name = product.Name;
            productFound.Price = product.Price;
            productFound.Supplier = product.Supplier;
            productFound.SupplierId = product.SupplierId;
            return await _context.SaveChangesAsync() > 0;
        }
        else
        {
            return false;
        }

    }
}
