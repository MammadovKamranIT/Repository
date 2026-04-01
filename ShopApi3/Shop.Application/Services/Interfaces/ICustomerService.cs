using Shop.DTO.Customer_DTO;


namespace Shop.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllForUserAsync(string userId, IList<string> roles);
        Task<CustomerResponseDto?> GetByIdAsync(int id);
        Task<CustomerResponseDto?> UpdateAsync(int id, CustomerUpdateRequest updateRequest);
        Task<CustomerResponseDto> CreateAsync(CustomerCreateRequest createCustomerDto, string ownerId);

        Task<bool> DeleteAsync(int id);



    }
}
