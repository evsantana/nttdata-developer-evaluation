using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller for managing sales operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly ISaleService _saleService;

        /// <summary>
        /// Initializes a new instance of CartsController
        /// </summary>
        /// <param name="productService">The ICartService instance</param>
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Creates a new sale
        /// </summary>
        /// <param name="saleDTO">The sale creation DTO</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(SaleDTO saleDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _saleService.CreateAsync(saleDTO, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleDTO>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = saleDTO
            });

        }

        /// <summary>
        /// Cancel a sale
        /// </summary>
        /// <param name="id">Sale ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The canceled sale details</returns>
        [HttpPatch("cancel/{id:guid}")]
        public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _saleService.CancelAsync(id, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Sale cancelled successfully"
            });
        }

        /// <summary>
        /// Cancel a item sale
        /// </summary>
        /// <param name="saleId">Sale ID</param>
        /// <param name="productId">Product ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpPatch("cancel-item/{saleId:guid}/{productId:guid}")]
        public async Task<IActionResult> CancelItem([FromRoute] Guid saleId, [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _saleService.CancelItemAsync(saleId, productId, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Item cancelled successfully"
            });
        }

        /// <summary>
        /// Retrieves a sale by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            var sale = await _saleService.GetByIdAsync(id, cancellationToken);

            if (sale is null)
                return NotFound(new ApiResponseWithData<SaleDTO>
                {
                    Message = "Sale not found"
                });

            return Ok(new ApiResponseWithData<SaleDTO>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = sale
            });
        }

        /// <summary>
        /// Retrieves all sales
        /// </summary>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order field</param>
        /// <param name="orderDirection">Order direction</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<PaginatedResponse<SaleDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllSales(
            int pageNumber = 1,
            int pageSize = 10,
            string orderBy = "saleNumber",
            string orderDirection = "asc",
            CancellationToken cancellationToken = default)
        {

            var result = await _saleService.GetPaginatedAsync(pageNumber, pageSize, orderBy, orderDirection, cancellationToken);

            return OkPaginated(result);
        }


    }
}
