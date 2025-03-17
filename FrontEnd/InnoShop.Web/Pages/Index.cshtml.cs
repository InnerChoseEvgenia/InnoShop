using InnoShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson.IO;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Specs;


namespace InnoShop.Web.Pages
{
    public class IndexModel(IProductService productService, ILogger<IndexModel> logger)
    : PageModel
    {
        //public IEnumerable<ProductModel> ProductList { get; set; } = Enumerable.Empty<ProductModel>();

        //= new List<ProductModel>();
        public IEnumerable<Products> ProductList { get; set; } = new List<Products>();
        //public IList<ProductResponse> ProductListt { get; set; }
        public CatalogSpecParams CatalogSpecParams { get; set; } = new();
        //= new List<CatalogSpecParams>();
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    logger.LogInformation("Index page visited");
        //    var result = await productService.GetAllProducts();
        //    //var result = await productService.GetProducts(2, 3);
        //    ProductList = result.Products;
        //    return Page();
        //}
        //[FromQuery]
        //CatalogSpecParams catalogSpecParams
        //new GetAllProductsQuery(catalogSpecParams)
        //public IEnumerable<Products> ProductList { get; set; } = new List<Products>();

        public async Task OnGet(CatalogSpecParams catalogSpecParams)
        {
            logger.LogInformation("Index page visited");
            ProductList = (IEnumerable<Products>)await productService.GetAllProductsAsync(catalogSpecParams);
        }
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    logger.LogInformation("Index page visited"); 
        //    var result = await productService.GetProducts();
        //    ProductList = (IEnumerable<ProductModel>)result.Products;
        //    return Page();
        //}
        // public List<ProductModel> ProductList { get; set; }
        //public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
//        public async Task OnGetAsync()
//        {
//            var pagination = await productService.GetAllProducts();
//            ProductList = (IEnumerable<ProductModel>)(pagination?.Products);
////ToList() ?? new List<ProductModel>(); 
                
//        }
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    logger.LogInformation("Index page visited");

        //    //ProductList = await productService.GetAllProducts();

        //    // Call the product service with the catalogSpecParams

        //    var result = await productService.GetAllProducts();
        //    ProductList = result?.Products ?? Enumerable.Empty<ProductModel>();
        //    // Assuming result.Products is a property of the response
        //    //ProductList = result.Products;

        //    return Page();
        //}

    }
}
