namespace Product.Infrastructure.Data
{
    public interface IProductContext
    {
        IMongoCollection<Products> Product { get; }
        IMongoCollection<ProductAuthor> Authors { get; }
        IMongoCollection<ProductType> Types { get; }
    }
}

