
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Product.Core.Entities;
using System.Text.Json;

namespace Product.Integration.Tests
{
    public class ProductIntegrationTests : IClassFixture<MongoDbFixture>
    {
        private readonly MongoDbFixture _fixture;
        private readonly HttpClient _client;

        public ProductIntegrationTests(MongoDbFixture fixture)
        {
            _fixture = fixture;
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll<IMongoClient>();
                        services.AddSingleton<IMongoClient>(
                            (_) => _fixture.Client);
                    });
                });
            _client = appFactory.CreateClient();
        }

      //  [Fact]
        public async Task GetAllProduct_ShouldReturnProducts()
        {
            // Arrange
            var productDb = _fixture.Client.GetDatabase("ProductDb");
            var collection = productDb.GetCollection<Product>("Products");
            await collection.InsertOneAsync(new Product("Securing",
                  "Ocelot API Gateway",
                   33, "images/products/adidas_shoe-2.png",
                   ".Net Microservices",
                   "Micross",
                   "Meht Oza",
                   true, new DateTime(2030, 1, 1)));

            // Act
            var res = await _client.GetAsync("/GetAllProducts");
            res.EnsureSuccessStatusCode();
            var content = await res.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<ICollection<Products>>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Assert
            Assert.NotNull(items);
            Assert.NotEmpty(items);
            Assert.Single(items);
            var resultItem = items.FirstOrDefault();
            Assert.NotNull(resultItem);
        }

        internal record Product (
            string Name, string Description, decimal Price, 
            string ImageFile, string Summary,
            string Author, string Types, 
            bool IsAvailable, DateTime CreateAt);


        public void Dispose()
        {
            var mangoMarketDb = _fixture.Client.GetDatabase("ProductDb");
            var collection = mangoMarketDb.GetCollection<Product>("Products");
            collection.DeleteManyAsync(_ => true).Wait();
        }
    }

}
