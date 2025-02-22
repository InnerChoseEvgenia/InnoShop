namespace InnoShop.Web.Models.User
{
    public class UserModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string JWT { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
    public class AuthorProductItem
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string Types { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public class AuthorProductList
    {
        public string UserName { get; set; }
        public List<AuthorProductItem> Items { get; set; } = new List<AuthorProductItem>();

        public AuthorProductList() { }
        public AuthorProductList(string userName)
        {
            UserName = userName;
        }
    }
    public record GetAuthorProductListResponse(AuthorProductList List);

    //public record GetAuthorProductListRequest(AuthorProductList List);
    public record CreateAuthorProductItemRequest(string UserName);
    public record CreateAuthorProductItemResponse(AuthorProductItem Item);
    public record UpdateAuthorProductItemRequest(string ProductName);
    public record UpdateAuthorProductItemResponse(AuthorProductItem Item);
    public record DeleteAuthorProductItemResponse(bool IsSuccess);
}
