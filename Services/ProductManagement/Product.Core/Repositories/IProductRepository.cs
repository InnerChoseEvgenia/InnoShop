namespace Product.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Products>> GetProducts(CatalogSpecParams catalogSpecParams);
        Task<Products> GetProductById(string id);
        Task<IEnumerable<Products>> GetProductsByName(string name);
        Task<IEnumerable<Products>> GetProductsByAuthor(string authorName);
        Task<Products> CreateProduct(Products product);
        Task<bool> UpdateProduct(Products product);
        Task<bool> DeleteProduct(string id);
    }
}
