using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class MockCustomerRepository : ICustomerRepository
{
    public Task<IReadOnlyList<Customer>> ListAllAsync()
    {
        var customers = MockData.Customers.OrderBy(c => c.LastName).ToList();
        return Task.FromResult<IReadOnlyList<Customer>>(customers);
    }

    public Task<Customer?> FindByIdAsync(int id)
    {
        Customer? customer = MockData.Customers.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(customer);
    }
}