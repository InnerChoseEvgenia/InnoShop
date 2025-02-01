using MediatR;
using User.Application.Responses;

namespace User.Application.Queries
{
    public class GetListByUserNameQuery : IRequest<AuthorProductListResponse>
    {
        public string UserName { get; set; }
        public GetListByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
