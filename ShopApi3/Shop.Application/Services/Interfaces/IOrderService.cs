
using Shop.DTO.Order;
using Shop.DTO.Order_DTOs;
using Shop.Models;

namespace Shop.Services.Interfaces
{
    public interface IOrderService
    {

        Task<IEnumerable<OrderResponseDto>> GetAllAsync();

  
        Task<IEnumerable<OrderResponseDto>> GetByCustomerIdAsync(int customerId);
        Task<OrderResponseDto?> GetByIdAsync(int id);

        Task<Order?> GetOrderEntityAsync(int id);
        Task<OrderResponseDto> CreateAsync(OrderCreateRequest createRequest);
        Task<OrderResponseDto?> UpdateAsync(int id, OrderUpdateRequest updateRequest);
        Task<bool> DeleteAsync(int id);


    }
}
