namespace Product.Infrastructure.Data
{
    public static class AuthorContextSeed
    {
        public static void SeedData(IMongoCollection<ProductAuthor> authorCollection)
        {
            bool checkAuthor = authorCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "author.json");
            if (!checkAuthor)
            {
                var authorData = File.ReadAllText(path);
                //var authorData = File.ReadAllText("../Product.Infrastructure/Data/SeedData/author.json");
                var authors = JsonSerializer.Deserialize<List<ProductAuthor>>(authorData);
                if (authors != null)
                {
                    foreach (var item in authors)
                    {
                        authorCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
