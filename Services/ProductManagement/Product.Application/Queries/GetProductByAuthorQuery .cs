namespace Product.Application.Queries
{
    public class GetProductByAuthorQuery : IRequest<IList<ProductResponse>>
    {
        public string AuthorName { get; set; }

        public GetProductByAuthorQuery(string authorName)
        {
            AuthorName = authorName;
        }
    }
}
