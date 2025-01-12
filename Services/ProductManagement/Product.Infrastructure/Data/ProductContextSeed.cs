namespace Product.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Products> productCollection)
        {
            bool checkProducts = productCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                //var productsData = File.ReadAllText("../Product.Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Products>>(productsData);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        productCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
