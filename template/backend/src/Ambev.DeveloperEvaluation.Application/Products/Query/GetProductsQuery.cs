namespace Ambev.DeveloperEvaluation.Application.Products.Query
{
    public class GetProductsQuery : ProductQuery
    {
        public GetProductsQuery(int page, int pageSize, string orderBy, string orderDirection, IDictionary<string, string> filters) 
            : base(page, pageSize, orderBy, orderDirection, filters)
        {
        }
    }
}
