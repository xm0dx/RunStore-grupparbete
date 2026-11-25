using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MockSalesOrderRepository : ISalesOrderRepository
{
    public Task<IReadOnlyList<SalesOrder>> ListAllAsync()
    {
        IReadOnlyList<SalesOrder> list = MockData.SalesOrders.OrderBy(s => s.OrderNumber).ToList();
        return Task.FromResult(list);
    }
}
