using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Query
{
    public abstract class SaleQuery : IRequest<PaginatedList<Sale>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; } = "SaleNumber";
        public string OrderDirection { get; set; } = "asc";
        public SaleQuery(int page, int pageSize, string orderyBy, string orderDirection)
        {
            CurrentPage = page;
            PageSize = pageSize;
            OrderBy = orderyBy;
            OrderDirection = orderDirection;
        }
    }
}
