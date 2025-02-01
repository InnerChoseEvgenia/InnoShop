using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IAthorListRepository
    {
        Task<AuthorProductList> GetProductList (string userName);
        Task<AuthorProductList> UpdateProductList(AuthorProductList productList);
        Task DeleteProductList(string userName);
    }
}
