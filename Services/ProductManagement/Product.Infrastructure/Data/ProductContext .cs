using Microsoft.Extensions.Configuration;

namespace Product.Infrastructure.Data
{
    public class ProductContext : IProductContext
    {
        public IMongoCollection<ProductType> Types { get; }

        public IMongoCollection<Products> Product { get; }

        public IMongoCollection<ProductAuthor> Authors { get; } 

        public ProductContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Authors = database.GetCollection<ProductAuthor>(configuration.GetValue<string>("DatabaseSettings:AuthorsCollection"));
            Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
            Product = database.GetCollection<Products>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            AuthorContextSeed.SeedData(Authors);
            TypeContextSeed.SeedData(Types);
            ProductContextSeed.SeedData(Product);
        }
    }
}
