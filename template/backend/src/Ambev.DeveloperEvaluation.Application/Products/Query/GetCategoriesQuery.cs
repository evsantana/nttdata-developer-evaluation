using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Query
{
    public class GetCategoriesQuery : IRequest<IEnumerable<string>>
    {
    }
}
