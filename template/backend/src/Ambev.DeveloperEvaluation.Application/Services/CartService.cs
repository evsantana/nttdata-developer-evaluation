using Ambev.DeveloperEvaluation.Application.Carts.Command;
using Ambev.DeveloperEvaluation.Application.Carts.Query;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Common.Pagination;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class CartService : ICartService
    {
        #region Properties and Constructors
        private readonly IMediator _mediator;
        private IMapper _mapper;

        public CartService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion

        public async Task CreateAsync(CartDTO entity, CancellationToken cancellationToken)
        {
            var cartCreateCommand = _mapper.Map<CreateCartCommand>(entity);
            await _mediator.Send(cartCreateCommand, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var cartDeleteCommand = new DeleteCartCommand(id);

            if (cartDeleteCommand is null)
                throw new Exception($"Entity could not be loaded");

            await _mediator.Send(cartDeleteCommand, cancellationToken);
        }

        public Task<IEnumerable<CartDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cartByIdQuery = new GetCartByIdQuery(id);

            if (cartByIdQuery is null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(cartByIdQuery, cancellationToken);

            return _mapper.Map<CartDTO>(result);
        }

        public async Task<PaginatedList<CartDTO>> GetPaginatedAsync(int currentPage, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
        {
            var cartsQuery = new GetCartQuery(currentPage, pageSize, orderBy, orderDirection);

            var result = await _mediator.Send(cartsQuery, cancellationToken);

            var cartDTOs = _mapper.Map<IEnumerable<CartDTO>>(result);

            return new PaginatedList<CartDTO>(cartDTOs.ToList(), result.TotalCount, result.CurrentPage, result.PageSize);
        }

        public async Task UpdateAsync(CartDTO entity, CancellationToken cancellationToken)
        {
            var cartUpdateCommand = _mapper.Map<UpdateCartCommand>(entity);

            await _mediator.Send(cartUpdateCommand, cancellationToken);
        }
    }
}
