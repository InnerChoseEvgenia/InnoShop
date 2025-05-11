using User.Core.Entities;

namespace User.Application.Responses
{
    public class AuthorProductListResponse
    {
        public string UserName { get; set; }
        public List<AuthorProductItemResponse> Items { get; set; } = new List<AuthorProductItemResponse>();
        public AuthorProductListResponse()
        {

        }
        public AuthorProductListResponse(string username)
        {
            UserName = username;
        }

    }
}
