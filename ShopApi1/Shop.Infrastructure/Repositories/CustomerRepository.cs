using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Interfaces;
using Shop.Data;
using Shop.Models;


namespace Shop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ShopDbContext _context;

    public CustomerRepository(ShopDbContext context) => _context = context;

    public async Task<Customer> AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        await _context.Entry(customer).Collection(c => c.Orders).LoadAsync();
        return customer;
    }

    public async Task<Customer?> FindAsync(int id) => await _context.Customers.FindAsync([id]);

  

    public async Task RemoveAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }
}
