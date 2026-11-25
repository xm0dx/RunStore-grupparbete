using Core.Entities;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<IReadOnlyList<Customer>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}