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
}
