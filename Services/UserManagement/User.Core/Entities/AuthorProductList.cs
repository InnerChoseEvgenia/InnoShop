namespace User.Core.Entities
{
    public class AuthorProductList
    {
        public string UserName { get; set; }
        public List <AuthorProductItem> Items { get; set; }= new List<AuthorProductItem>();

        public AuthorProductList() { }
        public AuthorProductList(string userName)
        {
            UserName = userName;
        }
    }
}
