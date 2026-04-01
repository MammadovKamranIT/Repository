using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Common;
using Shop.DTO.Order;
using Shop.DTO.Order_DTOs;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers

{
    

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserOrAbove")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;

        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetAll()
        {
            var Orders = await _OrderService.GetAllAsync();

            return Ok(
                ApiResponse<IEnumerable<OrderResponseDto>>
                    .SuccessResponse(Orders, "Order retrieved successfully")
            );
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<OrderResponseDto>>> GetById(int id)
        {
            var Order = await _OrderService.GetByIdAsync(id);

            if (Order is null)
                return NotFound(
                    ApiResponse<OrderResponseDto>
                        .ErrorResponse($"Order item with ID {id} not found")
                );

            return Ok(
                ApiResponse<OrderResponseDto>
                    .SuccessResponse(Order, "Order found")
            );
        }

        /// <param name="customerId">Customer identifier</param>
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>> GetByCustomerId(int customerId)
        {
            var Orders = await _OrderService.GetByCustomerIdAsync(customerId);

            if (Orders == null || !Orders.Any())
                return NotFound(
                    ApiResponse<IEnumerable<OrderResponseDto>>
                        .ErrorResponse($"Orders for customer with ID {customerId} not found")
                );

            return Ok(
                ApiResponse<IEnumerable<OrderResponseDto>>
                    .SuccessResponse(Orders, $"Order items for customer {customerId} retrieved successfully")
            );
        }

        /// <summary>
        /// Creates a new task item.
        /// </summary>
        /// <param name="createRequest">Data for creating a task item</param>
        /// <returns>Created task item</returns>
        /// <response code="201">Task item successfully created</response>
        /// <response code="400">Invalid request data or validation error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Create([FromBody] OrderCreateRequest createRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ApiResponse<OrderResponseDto>
                        .ErrorResponse("Invalid request data")
                );

            try
            {
                var createdOrder = await _OrderService.CreateAsync(createRequest);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdOrder.Id },
                    ApiResponse<OrderResponseDto>
                        .SuccessResponse(createdOrder, "Order created successfully")
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    ApiResponse<OrderResponseDto>
                        .ErrorResponse(ex.Message)
                );
            }
        }

        /// <summary>
        /// Updates an existing task item.
        /// </summary>
        /// <param name="id">Task item identifier to update</param>
        /// <param name="updateRequest">Data for updating the task item</param>
        /// <returns>Updated task item</returns>
        /// <response code="200">Task item successfully updated</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Task item with the specified identifier not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Update(int id, [FromBody] OrderUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ApiResponse<OrderResponseDto>
                        .ErrorResponse("Invalid request data")
                );

            var updatedOrder = await _OrderService.UpdateAsync(id, updateRequest);

            if (updatedOrder is null)
                return NotFound(
                    ApiResponse<OrderResponseDto>
                        .ErrorResponse($"Order with ID {id} not found")
                );

            return Ok(
                ApiResponse<OrderResponseDto>
                    .SuccessResponse(updatedOrder, "Order updated successfully")
            );
        }

        /// <summary>
        /// Deletes a task item by its identifier.
        /// </summary>
        /// <param name="id">Task item identifier to delete</param>
        /// <returns>Result of the delete operation</returns>
        /// <response code="200">Task item successfully deleted</response>
        /// <response code="404">Task item with the specified identifier not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var isDeleted = await _OrderService.DeleteAsync(id);

            if (!isDeleted)
                return NotFound(
                    ApiResponse<object>
                        .ErrorResponse($"Order with ID {id} not found")
                );

            return Ok(
                ApiResponse<object>
                    .SuccessResponse(null, "Order deleted successfully")
            );
        }
    }




    
}
