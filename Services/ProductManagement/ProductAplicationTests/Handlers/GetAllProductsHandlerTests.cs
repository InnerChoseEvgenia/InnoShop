using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;
using Product.Core.Specs;


namespace ProductAplicationTests.Handlers
{
    public class GetAllProductsHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetAllProductsHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsProductResponse()
        {
            // Arrange
            var products = new List<Products>()
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
              },
              new ()
            {

            Id = "302d2149e773f2a3990b47f5",
            Name = "CQRS",
            Description= "CQRS",
            Price= 29.99m,
            ImageFile="images/products/adidas_shoe-2.png",
            Summary="CQRS, Vertical/Clean Architecture",
            Types = new ProductType()
              {
                  Id="73ca5d6d958e43ee1cd375fe",
                  Name="Microservices"
               },
            Author= new ProductAuthor ()
              {
                Id="73ca5e4c455900b990b43bc1",
                Name= "Mehmet Ozkaya"
              },
             IsAvailable= true,
             CreateAt=new DateTime(2031, 1, 1)
              }
            };
            var catalogSpecParams = new CatalogSpecParams
            {
                PageIndex = 2,
                PageSize = 20,
                AuthorId = "author123",
                TypeId = "type456",
                Sort = "asc",
                Search = "example search"
            };


            var expectedProducts = new Pagination<Products>
            {
                PageIndex = 2,
                PageSize = 20,
                Count = 2,
                Data = products.ToList()
            };

            var query = new GetAllProductsQuery(catalogSpecParams);
            
            _productRepositoryMock
                .Setup(repo => repo.GetProducts(query.CatalogSpecParams))
                .ReturnsAsync(expectedProducts);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Pagination<ProductResponse>>(result);
        }

        [Fact]
        public async Task Handle_ProductRepositoryReturnsNull_ReturnsNullProductResponse()
        {
            // Arrange
            var catalogSpecParams = new CatalogSpecParams
            {
                PageIndex = 2,
                PageSize = 20,
                AuthorId = "author123",
                TypeId = "type456",
                Sort = "asc",
                Search = "example search"
            };
            var query = new GetAllProductsQuery(catalogSpecParams);
            

            _productRepositoryMock
                .Setup(repo => repo.GetProducts(query.CatalogSpecParams))
                .ReturnsAsync((Pagination<Products>)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

    }
}
