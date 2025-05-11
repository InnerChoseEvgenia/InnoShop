using Product.Application.Commands;
using Product.Core.Entities;

namespace ProductAplicationTests
{
    public static class ProductParamData
    {
        public static IEnumerable<object[]> GetCreateProductCommand()
        {
            yield return new object[]
            {
                new CreateProductCommand()
                {
                    Name = "NET 8 DDD, CQRS",
                    Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
                    Price = 29.99m,
                    ImageFile = "images/products/adidas_shoe-1.png",
                    Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
                    Types = new ProductType()
                    {
                        Id = "63ca5d6d958e43ee1cd375fe",
                        Name = "Microservices"
                    },
                    Author = new ProductAuthor()
                    {
                        Id = "63ca5e4c455900b990b43bc1",
                        Name = "Mehmet Ozkaya"
                    },
                    IsAvailable = true,
                    CreateAt = new DateTime(2030, 1, 1)
                }
            };
        }
    }
}
