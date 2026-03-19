using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Interfaces;
using Shop.Data;
using Shop.Models;


namespace Shop.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopDbContext _context;

    public OrderRepository(ShopDbContext context) => _context = context;

    public async Task<Order> AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        await _context.Entry(order).Reference(o => o.customer).LoadAsync();
        return order;
    }

    public async Task<Order?> FindAsync(int id) => await _context.Orders.FindAsync(id);

    public async Task<Order?> GetByIdWithCustomerAsync(int id) =>
        await _context.Orders.Include(o => o.customer).FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetAllWithCustomerAsync() =>
        await _context.Orders.Include(o => o.customer).ToListAsync();

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId) =>
        await _context.Orders.Include(o => o.customer).Where(o => o.CustomerId == customerId).ToListAsync();

    public async Task<bool> CustomerExistsAsync(int customerId) => await _context.Customers.AnyAsync(c => c.Id == customerId);

  
    public async Task RemoveAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
