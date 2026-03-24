using Shop.Models;



namespace Shop.Application.Services.Interfaces
{
    public interface ICustomerRepository
    {



        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(Customer customer);
        Task<Customer?> FindAsync(int id);
    }
}
