using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

// samma mönster som ProductRepository
public class SupplierRepository(RunStoreDbContext context) : ISupplierRepository
{
    public async Task<IReadOnlyList<Supplier>> ListAllAsync()
    {
        return await context.Suppliers.OrderBy(s => s.Name).ToListAsync();
    }

    public async Task<Supplier?> FindByIdAsync(int id)
    {
        return await context.Suppliers.FindAsync(id);
    }

    public async Task<bool> AddAsync(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
        // SaveChanges returnerar antal rader som skrevs
        return await context.SaveChangesAsync() > 0;
    }
}
