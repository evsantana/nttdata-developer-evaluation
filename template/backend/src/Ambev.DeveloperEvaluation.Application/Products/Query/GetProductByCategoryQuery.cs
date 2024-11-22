namespace Ambev.DeveloperEvaluation.Application.Products.Query
{
    public class GetProductByCategoryQuery : ProductQuery
    {
        public string CategoryName { get; set; }

        public GetProductByCategoryQuery(string categoryName, int page, int pageSize) : base(page, pageSize, "", "")
        {
            CategoryName = categoryName;
        }
    }
}
