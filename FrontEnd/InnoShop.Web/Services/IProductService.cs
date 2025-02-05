using InnoShop.Web.Models.Product;
using Refit;

namespace InnoShop.Web.Services
{
    public interface IProductService
    {
        [Get("/product-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<GetProductsResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10);

        [Get("/product-service/products/{id}")]
        Task<GetProductByIdResponse> GetProduct(string id);

        [Get("/product-service/products/productType/{type}")]
        Task<GetProductByTypeResponse> GetProductsByType(ProductType type);
        [Get("/product-service/products/productAuthor/{author}")]
        Task<GetProductByAuthorResponse> GetProductsByAuthor(ProductAuthor author);
        [Get("/product-service/products/productName/{name}")]
        Task<GetProductByNameResponse> GetProductsByName(string name);
        
    }
}
