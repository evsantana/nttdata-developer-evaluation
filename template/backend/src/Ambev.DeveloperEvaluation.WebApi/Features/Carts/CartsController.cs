using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{

    /// <summary>
    /// Controller for managing carts operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// Initializes a new instance of CartsController
        /// </summary>
        /// <param name="productService">The ICartService instance</param>
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Creates a new cart
        /// </summary>
        /// <param name="cartDto">The cart creation DTO</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created cart details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CartDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CartDTO cartDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _cartService.CreateAsync(cartDTO, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CartDTO>
            {
                Success = true,
                Message = "Cart created successfully",
                Data = cartDTO
            });

        }

        /// <summary>
        /// Deletes a cart by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the cart to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the cart was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            var cart = await _cartService.GetByIdAsync(id, cancellationToken);

            if (cart is null)
                return NotFound(new ApiResponseWithData<CartDTO>
                {
                    Message = "Cart not found"
                });

            await _cartService.DeleteAsync(id, cancellationToken);

            return Ok(new ApiResponseWithData<CartDTO>
            {
                Success = true,
                Message = "Cart deleted successfully"
            });
        }

        /// <summary>
        /// Updates a cart
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <param name="request">Cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the cart was updated</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CartDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound(new ApiResponseWithData<CartDTO> { Message = "ID invalid" });

            if (request is null)
                return NotFound(new ApiResponseWithData<CartDTO> { Message = "Invalid data" });

            await _cartService.UpdateAsync(request, cancellationToken);

            return Ok(new ApiResponseWithData<CartDTO>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = request
            });
        }

        /// <summary>
        /// Retrieves a cart by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            var cart = await _cartService.GetByIdAsync(id, cancellationToken);

            if (cart is null)
                return NotFound(new ApiResponseWithData<CartDTO>
                {
                    Message = "Cart not found"
                });

            return Ok(new ApiResponseWithData<CartDTO>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = cart
            });
        }


        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order field</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<PaginatedResponse<CartDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCarts(
            int pageNumber = 1,
            int pageSize = 10,
            string orderBy = "userId",
            string orderDirection = "asc",
            CancellationToken cancellationToken = default)
        {

            orderBy = orderBy.Trim().ToLower() == "date" ? "CreatedAt" : orderBy.Trim();

            var result = await _cartService.GetPaginatedAsync(pageNumber, pageSize, orderBy, orderDirection, cancellationToken);

            return OkPaginated(result);
        }
    }
}
