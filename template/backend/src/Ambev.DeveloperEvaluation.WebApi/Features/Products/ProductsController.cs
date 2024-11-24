using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{

    /// <summary>
    /// Controller for managing products operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {

        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of ProductsController
        /// </summary>
        /// <param name="productService">The IProductService instance</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productDto">The product creation DTO</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ProductDTO productDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _productService.CreateAsync(productDto, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<ProductDTO>
            {
                Success = true,
                Message = "Product created successfully",
                Data = productDto
            });

        }

        /// <summary>
        /// Retrieves a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            var product = await _productService.GetByIdAsync(id, cancellationToken);

            if (product is null)
                return NotFound(new ApiResponseWithData<ProductDTO>
                {
                    Message = "Product not found"
                });

            return Ok(new ApiResponseWithData<ProductDTO>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = product
            });
        }

        /// <summary>
        /// Deletes a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            var product = await _productService.GetByIdAsync(id, cancellationToken);

            if (product is null)
                return NotFound(new ApiResponseWithData<ProductDTO>
                {
                    Message = "Product not found"
                });

            await _productService.DeleteAsync(id, cancellationToken);

            return Ok(new ApiResponseWithData<ProductDTO>
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="request">Product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was updated</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound(new ApiResponseWithData<ProductDTO> { Message = "ID invalid" });

            if (request is null)
                return NotFound(new ApiResponseWithData<ProductDTO> { Message = "Invalid data" });

            await _productService.UpdateAsync(request, cancellationToken);

            return Ok(new ApiResponseWithData<ProductDTO>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = request
            });
        }

        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<PaginatedResponse<ProductDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts(
            int pageNumber = 1, 
            int pageSize = 10, 
            string orderBy = "title",
            [FromQuery] IDictionary<string, string>? filters = null,
            string orderDirection = "asc", 
            CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetPaginateOrderedAsync(pageNumber, pageSize, orderBy, orderDirection, filters, cancellationToken);

            return OkPaginated(result);
        }

        /// <summary>
        /// Retrieves a paginated list of entities in a category
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories paginated</returns>
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(ApiResponseWithData<PaginatedResponse<ProductDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] string category, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetByCategoryPaginatedAsync(category, pageNumber, pageSize, cancellationToken);

            return OkPaginated(result);
        }

        /// <summary>
        /// Retrieve all product categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories</returns>
        [HttpGet("categories/")]
        [ProducesResponseType(typeof(ApiResponseWithData<PaginatedResponse<string[]>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductsByCategory(CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetCategoriesAsync(cancellationToken);

            return Ok(result);
        }


    }
}
