using Microsoft.AspNetCore.Mvc;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Specs;
using Refit;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace InnoShop.Web.Services
{
    
    public interface IProductService
    {
        //[Get("/product-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
        //[Get("/product-service/api/v1/Product/GetAllProducts}")]

        //"?pageNumber={pageNumber}&pageSize={pageSize}")]
        //"?PageIndex={pageIndex}&PageSize={pageSize}&AuthorId={authorId}&TypeId={typeId}&Sort={sort}&Search={search}")]
        //"GetAllProducts/?CatalogSpecParams ={catalogSpecParams}")]
        //"?CatalogSpecParams ={catalogSpecParams}")]
        //"?PageIndex={pageIndex}&PageSize={pageSize}")]
        //"&AuthorId={authorId}&TypeId={typeId}&Sort={sort}&Search={search}")]IEnumerable<CatalogSpecParams>? catalogSpecParams?CatalogSpecParams ={catalogSpecParams}
        //Task<IList<GetProductsResponse>> GetAllProducts([Query] CatalogSpecParams catalogSpecParams);
        //Task<GetProductsResponse> GetAllProducts(GetAllProductsQuery request);int? pageNumber = 1, int? pageSize = 10
        //Task <ProductModel> GetAllProducts(); http://localhost:6002/api/v1/Product/GetAllProducts
        //http://localhost:6002/api/v1/Product/GetAllProducts?PageIndex=2&PageSize=2

        //Task<GetProductsResponse> GetAllProducts();[Query] CatalogSpecParams catalogSpecParams
        [Get("/product-service/api/v1/Product/GetAllProducts")]
        //Task <Pagination<ProductModel>> ProductResponse
        //Task<List<ProductModel>> GetProducts(int? pageIndex = 1, int? pageSize = 10);

        Task <IEnumerable<ProductResponse>> GetAllProductsAsync([Query] CatalogSpecParams catalogSpecParams);
        //Task<GetProductsResponse> GetAllProducts();
        ////string? authorName, string? type, string? sort, string? search,

        //[Get("/product-service/api/v1/Product/GetAllAuthor")]
        //Task<GetProductsResponse> GetAllAuthor();

        //[Get("/product-service/api/v1/Product/GetAllTypes")]
        //Task<GetProductsResponse> GetAllTypes();

        //[Get("/product-service/products/{id}")]
        //[Get("/product-service/api/v1/Product/GetProductById/{id}")]
        //Task<GetProductByIdResponse> GetProduct(string id);

        ////[Get("/product-service/products/productType/{type}")]
        ////Task<GetProductByTypeResponse> GetProductsByType(ProductType type);

        ////[Get("/product-service/products/productAuthor/{author}")]
        //[Get("/product-service/api/v1/Product/GetProductsByAuthorName/{author}")]
        //Task<GetProductByAuthorResponse> GetProductsByAuthor(ProductAuthor author);

        ////[Get("/product-service/products/productName/{name}")]
        //[Get("/product-service/api/v1/Product/GetProductByProductName/{name}")]
        //Task<GetProductByNameResponse> GetProductsByName(string name);
        
    }
}
