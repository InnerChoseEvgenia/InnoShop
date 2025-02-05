namespace InnoShop.Web.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public ProductAuthor Author { get; set; }
        public ProductType Types { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public class ProductAuthor 
    {
       public string Name { get; set; }
    }
    public class ProductType

    { 
        public string Name { get; set; } 
    }
        
    public record GetProductsResponse(IEnumerable<ProductModel> Products);
    public record GetProductByAuthorResponse(IEnumerable<ProductModel> Products);

    public record GetProductByTypeResponse(IEnumerable<ProductModel> Products);
    public record GetProductByNameResponse(ProductModel Product);
    public record GetProductByIdResponse(ProductModel Product);
}

