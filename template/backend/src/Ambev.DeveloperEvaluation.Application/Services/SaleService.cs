using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Sales.Command;
using Ambev.DeveloperEvaluation.Application.Sales.Query;
using Ambev.DeveloperEvaluation.Common.Pagination;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {

        #region Properties and Constructors
        private readonly IMediator _mediator;
        private IMapper _mapper;

        public SaleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion

        public async Task CreateAsync(SaleDTO entity, CancellationToken cancellationToken)
        {
            var saleCreateCommand = _mapper.Map<CreateSaleCommand>(entity);
            await _mediator.Send(saleCreateCommand, cancellationToken);
        }

        public async Task CancelAsync(Guid id, CancellationToken cancellationToken)
        {
            var saleCancelCommand = _mapper.Map<CancelSaleCommand>(id);
            await _mediator.Send(saleCancelCommand, cancellationToken);
        }

        public async Task CancelItemAsync(Guid id, Guid itemId, CancellationToken cancellationToken)
        {
            var saleCancelItemCommand = new CancelSaleItemCommand(id, itemId);
            await _mediator.Send(saleCancelItemCommand, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var saleDeleteCommand = new DeleteSaleCommand(id);

            if (saleDeleteCommand is null)
                throw new Exception($"Entity could not be loaded");

            await _mediator.Send(saleDeleteCommand, cancellationToken);
        }

        public Task<IEnumerable<SaleDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SaleDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var saleByIdQuery = new GetSaleByIdQuery(id);

            if (saleByIdQuery is null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(saleByIdQuery, cancellationToken);

            return _mapper.Map<SaleDTO>(result);
        }

        public async Task<PaginatedList<SaleDTO>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
        {
            var salesQuery = new GetSaleQuery(currentPage, pageSize, orderBy, orderDirection);

            var result = await _mediator.Send(salesQuery, cancellationToken);

            var saleDTO = _mapper.Map<IEnumerable<SaleDTO>>(result);

            return new PaginatedList<SaleDTO>(saleDTO.ToList(), result.TotalCount, result.CurrentPage, result.PageSize);

        }

        public async Task UpdateAsync(SaleDTO entity, CancellationToken cancellationToken)
        {
            var saleUpdateCommand = _mapper.Map<UpdateSaleCommand>(entity);

            await _mediator.Send(saleUpdateCommand, cancellationToken);
        }
    }
}
