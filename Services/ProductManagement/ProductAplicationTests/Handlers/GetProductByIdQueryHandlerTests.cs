using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    //[TestClass()]
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_mockProductRepository.Object);
        }


        [Fact]
        public async Task Handle_ExistingProductId_ReturnsProductResponse()
        {
            // Arrange
            var productId = "202d2149e773f2a3990b47f5"; 
            var product = new Products 
            {
                Id = "202d2149e773f2a3990b47f5",
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
            };

            _mockProductRepository.Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync(product);

            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id); 
            Assert.Equal(product.Name, result.Name); 
        }

        [Fact]
        public async Task Handle_NonExistingProductId_ReturnsNull()
        {
            // Arrange
            var productId = "202d2149e773f2a3990b47f5"; 
            var product = new Products 
            {
                Id = "202d2149e773f2a3990b47f5",
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
            };
            _mockProductRepository.Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync((Products)null); 

            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result); 
        }

        private static List<ProductResponse> ProductsDataBase()
        {
            return new List<ProductResponse>()
            {
             new ()
            {

            Id = "202d2149e773f2a3990b47f5",
            Name = "NET 8 DDD, CQRS",
            Description= ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Price= 29.99m,
            ImageFile="images/products/adidas_shoe-1.png",
            Summary=".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Types = new ProductType()
              {
                  Id="63ca5d6d958e43ee1cd375fe",
                  Name="Microservices"
               },
            Author= new ProductAuthor ()
              {
                Id="63ca5e4c455900b990b43bc1",
                Name= "Mehmet Ozkaya"
              },
             IsAvailable= true,
             CreateAt=new DateTime(2030, 1, 1)
              }
            };
        }
    }
}
        

