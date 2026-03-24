using AutoMapper;
using Shop;
using Shop.DTO.Customer_DTO;
using Shop.DTO.Customer_DTOs;
using Shop.DTO.Order_DTOs;
using Shop.Models;
using Shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Shop.Application.Services.Interfaces;

namespace Shop.Services
{
    public class CustomerService : ICustomerService
    {


        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
 
            IMapper mapper)
        {
            _customerRepository = customerRepository;
         
            _mapper = mapper;
        }



        public async Task<CustomerResponseDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            return customer is null ? null : _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<CustomerResponseDto> CreateAsync(CustomerCreateRequest createCustomerDto, string ownerId)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);

            await _customerRepository.AddAsync(customer);
            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<CustomerResponseDto?> UpdateAsync(int id, CustomerUpdateRequest updateCustomerDto)
        {
            var customer = await _customerRepository.FindAsync(id);
            if (customer is null) return null;
            _mapper.Map(updateCustomerDto, customer);
            await _customerRepository.UpdateAsync(customer);
            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _customerRepository.FindAsync(id);
            if (customer is null) return false;
            await _customerRepository.RemoveAsync(customer);
            return true;
        }


     

    }
}
