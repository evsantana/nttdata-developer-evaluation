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

        public string OrderBy { get; set; } = "Title"; //Default order field
        public string OrderDirection { get; set; } = "asc"; //Default order direction

        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        public ProductQuery(int page, int pageSize, string orderBy, string orderDirection)
        {
            CurrentPage = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderDirection = orderDirection;
        }

        public ProductQuery(int page, int pageSize, string orderBy, string orderDirection, IDictionary<string, string> filters)
        {
            CurrentPage = page;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderDirection = orderDirection;
            Filters = filters;
        }
    }
}
