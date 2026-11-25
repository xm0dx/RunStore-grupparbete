using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

// samma gränssnitt som SupplierRepository men jobbar mot listan i MockData istället för databasen
public class MockSupplierRepository : ISupplierRepository
{
    public Task<IReadOnlyList<Supplier>> ListAllAsync()
    {
        IReadOnlyList<Supplier> list = MockData.Suppliers.OrderBy(s => s.Name).ToList();
        return Task.FromResult(list);
    }

    public Task<Supplier?> FindByIdAsync(int id)
    {
        return Task.FromResult(MockData.Suppliers.FirstOrDefault(s => s.Id == id));
    }

    public Task<bool> AddAsync(Supplier supplier)
    {
        // nästa lediga id, som en databas skulle göra
        supplier.Id = MockData.Suppliers.Count == 0 ? 1 : MockData.Suppliers.Max(s => s.Id) + 1;
        MockData.Suppliers.Add(supplier);
        return Task.FromResult(true);
    }
}
