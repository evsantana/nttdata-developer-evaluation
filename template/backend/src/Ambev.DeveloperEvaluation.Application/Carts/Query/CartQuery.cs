using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.CartCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Query
{
    public abstract class CartQuery : IRequest<PaginatedList<Cart>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = "UserId";
        public string OrderDirection { get; set; } = "asc";
        public CartQuery(int page, int pageSize, string orderyBy, string orderDirection)
        {
            CurrentPage = page;
            PageSize = pageSize;
            OrderBy = orderyBy;
            OrderDirection = orderDirection;
        }
    }
}
