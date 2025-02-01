using MediatR;
using User.Application.Commands;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class DeleteProductListByUserNameHandler : IRequestHandler<DeleteProductListByUserNameCommand, Unit>
    {
        private readonly IAthorListRepository _athorListRepository;

        public DeleteProductListByUserNameHandler(IAthorListRepository athorListRepository)
        {
            _athorListRepository = athorListRepository;
        }
        public async Task<Unit> Handle(DeleteProductListByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _athorListRepository.DeleteProductList(request.UserName);
            return Unit.Value;
        }
    }
}
