using InnoShop.Web.Models.Product;
using InnoShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InnoShop.Web.Pages
{
    public class IndexModel(IProductService productService, IUserService userService, ILogger<IndexModel> logger)
    : PageModel
    {
        
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited");
            var result = await productService.GetProducts();
            //var result = await productService.GetProducts(2, 3);
            ProductList = result.Products;
            return Page();
        }
       
    }
}
