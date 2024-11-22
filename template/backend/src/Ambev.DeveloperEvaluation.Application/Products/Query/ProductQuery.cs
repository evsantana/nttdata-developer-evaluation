using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Query
{
    /// <summary>
    /// Abstract class for the base of queries
    /// </summary>
    public abstract class ProductQuery : IRequest<PaginatedList<Product>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public string OrderBy { get; set; } = "Title"; // Campo padrão para ordenação
        public string OrderDirection { get; set; } = "asc"; // Direção padrão (ascendente)

        public ProductQuery(int page, int pageSize, string orderBy, string orderDirection)
        {
            CurrentPage = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderDirection = orderDirection;
        }
    }
}
