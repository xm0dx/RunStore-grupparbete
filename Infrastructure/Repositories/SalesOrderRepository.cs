using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SalesOrderRepository(RunStoreDbContext context) : ISalesOrderRepository
{
    public async Task<IReadOnlyList<SalesOrder>> ListAllAsync()
    {
        return await context.SalesOrders.ToListAsync();
    }
}
