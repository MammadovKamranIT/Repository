using Shop.Common;
using Shop.DTO.Customer_DTO;
using Shop.Services;
using Shop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Shop.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserOrAbove")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IAuthorizationService _authorizationService;

        private string? UserId => User
                          .FindFirstValue(ClaimTypes.NameIdentifier);

        private IList<string> Roles => User
                                    .Claims
                                    .Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value)
                                    .ToList();

        public CustomersController(ICustomerService customerService, IAuthorizationService authorizationService)
        {
            _customerService = customerService;
            _authorizationService = authorizationService;
        }

        
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer is null)
                return NotFound(
                    ApiResponse<CustomerResponseDto>
                        .ErrorResponse($"Customer with ID {id} not found")
                );

            return Ok(
                ApiResponse<CustomerResponseDto>
                    .SuccessResponse(customer, "Customer found")
            );
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Create(
            [FromBody] CustomerCreateRequest createRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ApiResponse<CustomerResponseDto>
                        .ErrorResponse("Invalid request data")
                );

            var ownerId = UserId ?? throw new InvalidOperationException("User ID not found in claims");


            var createdCustomer = await _customerService.CreateAsync(createRequest, ownerId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCustomer.Id },
                ApiResponse<CustomerResponseDto>
                    .SuccessResponse(createdCustomer, "Customer created successfully")
            );
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CustomerResponseDto>>> Update(
            int id,
            [FromBody] CustomerUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ApiResponse<CustomerResponseDto>
                        .ErrorResponse("Invalid request data")
                );

            var updatedCustomer = await _customerService.UpdateAsync(id, updateRequest);

            if (updatedCustomer is null)
                return NotFound(
                    ApiResponse<CustomerResponseDto>
                        .ErrorResponse($"Customer with ID {id} not found")
                );

            return Ok(
                ApiResponse<CustomerResponseDto>
                    .SuccessResponse(updatedCustomer, "Customer updated successfully")
            );
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var isDeleted = await _customerService.DeleteAsync(id);

            if (!isDeleted)
                return NotFound(
                    ApiResponse<object>
                        .ErrorResponse($"Customer with ID {id} not found")
                );

            return Ok(
                ApiResponse<object>
                    .SuccessResponse(null, "Customer deleted successfully")
            );
        }
    }


}

