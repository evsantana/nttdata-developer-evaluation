namespace Ambev.DeveloperEvaluation.Application.Sales.Query
{
    public class GetSaleQuery : SaleQuery
    {
        public GetSaleQuery(int page, int pageSize, string orderyBy, string orderDirection)
            : base(page, pageSize, orderyBy, orderDirection)
        {
        }
    }
}
