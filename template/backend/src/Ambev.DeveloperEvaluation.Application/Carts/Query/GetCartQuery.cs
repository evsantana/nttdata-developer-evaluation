namespace Ambev.DeveloperEvaluation.Application.Carts.Query
{
    public class GetCartQuery : CartQuery
    {
        public GetCartQuery(int page, int pageSize, string orderyBy, string orderDirection)
            : base(page, pageSize, orderyBy, orderDirection)
        {
        }
    }
}
