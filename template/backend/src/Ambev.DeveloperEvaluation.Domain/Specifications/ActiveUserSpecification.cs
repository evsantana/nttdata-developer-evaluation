using Ambev.DeveloperEvaluation.Domain.Models.UserCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Models.UserCase.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
