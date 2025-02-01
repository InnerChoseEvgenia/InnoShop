using MediatR;
using User.Application.Mappers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class GetListByUserNameHandler : IRequestHandler<GetListByUserNameQuery, AuthorProductListResponse>
    {
        private readonly IAthorListRepository _authorListRepository;

        public GetListByUserNameHandler(IAthorListRepository authorListRepository)
        {
            _authorListRepository = authorListRepository;
        }
        public async Task<AuthorProductListResponse> Handle(GetListByUserNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _authorListRepository.GetProductList(request.UserName);
            var productListResponse = AuthorListMapper.Mapper.Map<AuthorProductListResponse>(productList);
            return productListResponse;
        }
    }
}
