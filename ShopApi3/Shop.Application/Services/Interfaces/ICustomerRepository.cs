using Shop.DTO.Customer_DTO;
using Shop.Models;



namespace Shop.Application.Services.Interfaces
{
    public interface ICustomerRepository
    {


        Task<IEnumerable<Customer>> GetAllForUserAsync(string userId, IList<string> roles);
        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(Customer customer);
        Task<Customer?> FindAsync(int id);
    }
}
