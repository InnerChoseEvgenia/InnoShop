using MediatR;

namespace User.Application.Commands
{
    public class DeleteProductListByUserNameCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public DeleteProductListByUserNameCommand(string userName)
        {
            UserName = userName;
        }
    }
}
