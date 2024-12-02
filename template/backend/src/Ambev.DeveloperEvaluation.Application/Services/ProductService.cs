﻿using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Products.Command;
using Ambev.DeveloperEvaluation.Application.Products.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class ProductService : IProductService
    {
        #region Properties and Constructors
        private readonly IMediator _mediator;
        private IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion

        public async Task CreateAsync(ProductDTO entity, CancellationToken cancellationToken)
        {
            var productCreateCommand = _mapper.Map<CreateProductCommand>(entity);
            await _mediator.Send(productCreateCommand, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var productDeleteCommand = new DeleteProductCommand(id);

            if (productDeleteCommand is null)
                throw new Exception($"Entity could not be loaded");

            await _mediator.Send(productDeleteCommand, cancellationToken);
        }

        public async Task UpdateAsync(ProductDTO entity, CancellationToken cancellationToken)
        {
            var productUpdateCommand = _mapper.Map<UpdateProductCommand>(entity);
            await _mediator.Send(productUpdateCommand, cancellationToken);
        }

        public async Task<PaginatedList<ProductDTO>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
        {
            //Create a query with pagination and sorting settings
            var productsQuery = new GetProductsQuery(currentPage, pageSize, orderBy, orderDirection, null);

            var result = await _mediator.Send(productsQuery, cancellationToken);

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(result);

            return new PaginatedList<ProductDTO>(productDTOs.ToList(), result.TotalCount, result.CurrentPage, result.PageSize);
        }

        /// <summary>
        /// Retrieves a paginated, ordered list of entities in a category
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="orderDirection">Order Direction</param>
        /// <param name="filters">Filters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of categories paginated</returns>
        public async Task<PaginatedList<ProductDTO>> GetPaginateOrderedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, IDictionary<string, string> filters, CancellationToken cancellationToken)
        {
            //Create a query with pagination, sorting and filters settings
            var productsQuery = new GetProductsQuery(currentPage, pageSize, orderBy, orderDirection, filters);

            var result = await _mediator.Send(productsQuery, cancellationToken);

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(result);

            return new PaginatedList<ProductDTO>(productDTOs.ToList(), result.TotalCount, result.CurrentPage, result.PageSize);
        }


        public async Task<ProductDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var productByIdQuery = new GetProductByIdQuery(id);

            if (productByIdQuery is null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(productByIdQuery, cancellationToken);

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<PaginatedList<ProductDTO>> GetByCategoryPaginatedAsync(string categoryName, int currentPage, int pageSize, CancellationToken cancellationToken)
        {
            var productsQuery = new GetProductByCategoryQuery(categoryName, currentPage, pageSize);

            var result = await _mediator.Send(productsQuery, cancellationToken);

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(result);

            return new PaginatedList<ProductDTO>(productDTOs.ToList(), result.TotalCount, result.CurrentPage, result.PageSize);

        }

        public async Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categoriesQuery = new GetCategoriesQuery();

            if (categoriesQuery is null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(categoriesQuery);

            return _mapper.Map<IEnumerable<string>>(result);
        }

        public Task<IEnumerable<ProductDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
