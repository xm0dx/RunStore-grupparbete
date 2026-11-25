using Core.Entities;


public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> ListAllAsync();
    Task<Customer?> FindByIdAsync(int id);
}