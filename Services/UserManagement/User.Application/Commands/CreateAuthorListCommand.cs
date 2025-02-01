using MediatR;
using User.Application.Responses;
using User.Core.Entities;

namespace User.Application.Commands
{
    public class CreateAuthorListCommand : IRequest<AuthorProductListResponse>
    {
        public string UserName { get; set; }
        public List<AuthorProductItem> Items { get; set; }
        public CreateAuthorListCommand(string username, List<AuthorProductItem> items)
        {
            UserName = username;
            Items = items;
        }
    }
}
