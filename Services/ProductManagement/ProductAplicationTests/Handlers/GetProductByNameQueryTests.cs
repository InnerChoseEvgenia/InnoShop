using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;
using FluentAssertions;
using Shouldly;

namespace ProductAplicationTests.Handlers
{
    public class GetProductByNameQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductByNameQueryHandler _handler;

        public GetProductByNameQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductByNameQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProductResponseList_WhenProductsFound()
        {
            // Arrange
            var name = "TestProduct";
            var query = new GetProductByNameQuery (name);
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

            new ProductResponse
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
        };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByName(query.Name))
                .ReturnsAsync(productList);


            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponseList);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsFound()
        {
            // Arrange
            var name = "NonExistentProduct";
            var query = new GetProductByNameQuery(name);
            var productList = new List<Products>();

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByName(query.Name))
                .ReturnsAsync(productList);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var name = "TestProduct";
            var query = new GetProductByNameQuery(name);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsByName(query.Name))
                .ThrowsAsync(new System.Exception("Repository exception"));

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<System.Exception>().WithMessage("Repository exception");
        }
    }
}
