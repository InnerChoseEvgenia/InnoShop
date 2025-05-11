using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    public class GetProductByAuthorHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductByAuthorHandler _handler;

        public GetProductByAuthorHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductByAuthorHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsProductResponseList()
        {
            // Arrange
            var authorName = "Author1";
            var query = new GetProductByAuthorQuery (authorName);
            var productList = new List<Products>
        {
            new Products 
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
             CreateAt=new DateTime(2030, 1, 1) },

            new Products 
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
             CreateAt=new DateTime(2031, 1, 1) },
        };
            var expectedResponseList = new List<ProductResponse>
        {
            new ProductResponse 
            {
            Id = "502d2149e773f2a3990b47f5",
            Name = "RS",
            Description= "RS",
            Price= 29.99m,
            ImageFile="images/products/adidas_shoe-2.png",
            Summary="CQRS, Vertical/Clean Architecture",
            Types = new ProductType()
              {
                  Id="53ca5d6d958e43ee1cd375fe",
                  Name="Microservices"
               },
            Author= new ProductAuthor ()
              {
                Id="53ca5e4c455900b990b43bc1",
                Name= "Mehmet Ozkaya"
              },
             IsAvailable= true,
             CreateAt=new DateTime(2031, 1, 1) },

            new ProductResponse 
            {
            Id = "602d2149e773f2a3990b47f5",
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
             CreateAt=new DateTime(2031, 1, 1) }
        };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByAuthor(authorName))
                .ReturnsAsync(productList);


            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponseList.Count, result.Count);
      
        }

        [Fact]
        public async Task Handle_NoProducts_ReturnsEmptyList()
        {
            // Arrange
            var authorName = "Author2";
            var query = new GetProductByAuthorQuery (authorName );
            var productList = new List<Products>();

            _productRepositoryMock
                 .Setup(repo => repo.GetProductsByAuthor(authorName))
                .ReturnsAsync(productList);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_ThrowsException_ReturnsError()
        {
            // Arrange
            var authorName = "Author3";
            var query = new GetProductByAuthorQuery ( authorName );

           
            _productRepositoryMock
                .Setup(repo => repo.GetProductsByAuthor(authorName))
                .ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(query, CancellationToken.None));
        }
    }
}
