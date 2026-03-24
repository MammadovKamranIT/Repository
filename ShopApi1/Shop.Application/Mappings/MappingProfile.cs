using AutoMapper;
using Shop.DTO.Customer_DTO;
using Shop.DTO.Order_DTOs;
using Shop.Models;
using Shop.DTO.Order;


namespace Shop.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            

            CreateMap<Customer, CustomerResponseDto>();


            CreateMap<CustomerCreateRequest, Customer>();


            CreateMap<CustomerUpdateRequest, Customer>();
            

            

            CreateMap<Order, OrderResponseDto>()
   
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap<OrderCreateRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                ;

            CreateMap<OrderUpdateRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                .ForMember(dest => dest.CustomerId, opt => opt.Ignore());
        }


    }
}
