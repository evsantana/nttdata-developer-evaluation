using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
    {
        #region Properties and Constructors
        private readonly DefaultContext _context;

        public SaleItemRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }
        #endregion
    }
}
