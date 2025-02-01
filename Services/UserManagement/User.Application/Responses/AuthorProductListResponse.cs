namespace User.Application.Responses
{
    public class AuthorProductListResponse
    {
        public string UserName { get; set; }
        public List<AuthorProductItemResponse> Items { get; set; }
        public AuthorProductListResponse()
        {

        }
        public AuthorProductListResponse(string username)
        {
            UserName = username;
        }
    }
}
