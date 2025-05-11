using MediatR;
using User.Application.Commands;
using User.Application.Mappers;
using User.Application.Responses;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class CreateAuthorListCommandHandler : IRequestHandler<CreateAuthorListCommand, AuthorProductListResponse>
    {
        private readonly IAthorListRepository _athorListRepository;

        public CreateAuthorListCommandHandler(IAthorListRepository athorListRepository)
        {
            _athorListRepository = athorListRepository;
        }
        public async Task<AuthorProductListResponse> Handle(CreateAuthorListCommand request, CancellationToken cancellationToken)
        {
            var authorProductList = await _athorListRepository.CreateProductList(new AuthorProductList
            {
                UserName = request.UserName,
                Items = request.Items
            });
            var authorProductListResponse = AuthorListMapper.Mapper.Map<AuthorProductListResponse>(authorProductList);
            return authorProductListResponse;
        }
    }
}
