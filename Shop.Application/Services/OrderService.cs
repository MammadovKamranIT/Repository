using AutoMapper;

using Shop.Application.Interfaces;
using Shop.Common;
using Shop.DTO.Order_DTOs;
using Shop.Models;
using Shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.DTO.Order;

namespace Shop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository OrderRepository, IMapper mapper)
        {
            _OrderRepository = OrderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> CreateAsync(OrderCreateRequest createOrder)
        {
            if (!await _OrderRepository.CustomerExistsAsync(createOrder.CustomerId))
                throw new ArgumentException($"Customer with ID {createOrder.CustomerId} not found");

            var Order = _mapper.Map<Order>(createOrder);
            var added = await _OrderRepository.AddAsync(Order);
            return _mapper.Map<OrderResponseDto>(added);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Order = await _OrderRepository.FindAsync(id);
            if (Order is null) return false;
            await _OrderRepository.RemoveAsync(Order);
            return true;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
        {
            var Orders = await _OrderRepository.GetAllWithCustomerAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(Orders);
        }

        public async Task<OrderResponseDto?> GetByIdAsync(int id)
        {
            var Order = await _OrderRepository.GetByIdWithCustomerAsync(id);
            return _mapper.Map<OrderResponseDto?>(Order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetByCustomerIdAsync(int customerId)
        {
            var Orders = await _OrderRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(Orders);
        }

        public async Task<OrderResponseDto?> UpdateAsync(int id, OrderUpdateRequest updateOrder)
        {
            var Order = await _OrderRepository.GetByIdWithCustomerAsync(id);
            if (Order is null) return null;
            _mapper.Map(updateOrder, Order);
            await _OrderRepository.UpdateAsync(Order);
            return _mapper.Map<OrderResponseDto>(Order);
        }


        public async Task<Order?> GetOrderEntityAsync(int id) =>
            await _OrderRepository.GetByIdWithCustomerAsync(id);



    }
}
