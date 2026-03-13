using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Shop.Application.Interfaces
{
    public interface ICustomerRepository
    {



        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(Customer customer);
        Task<Customer?> FindAsync(int id);
    }
}
