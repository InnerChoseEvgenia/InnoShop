namespace Product.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<ProductAuthor>> GetAllProductByAuthor();
    }
}
