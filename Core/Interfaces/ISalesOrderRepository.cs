using Core.Entities;

namespace Core.Interfaces;

public interface ISalesOrderRepository
{
    Task<IReadOnlyList<SalesOrder>> ListAllAsync();
}
