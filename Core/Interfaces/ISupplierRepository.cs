using Core.Entities;

namespace Core.Interfaces;

public interface ISupplierRepository
{
    Task<IReadOnlyList<Supplier>> ListAllAsync();
    Task<Supplier?> FindByIdAsync(int id);
    // true om leverantören sparades
    Task<bool> AddAsync(Supplier supplier);
}
