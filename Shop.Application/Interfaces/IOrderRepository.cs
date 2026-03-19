using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Interfaces
{
    public interface IOrderRepository
    {

        Task<bool> CustomerExistsAsync(int customerId);
        Task<Order> AddAsync(Order Order);
        Task<Order?> FindAsync(int id);
        Task<Order?> GetByIdWithCustomerAsync(int id);
        Task<IEnumerable<Order>> GetAllWithCustomerAsync();
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
        Task UpdateAsync(Order Order);
        Task RemoveAsync(Order Order);
     


    }
}
