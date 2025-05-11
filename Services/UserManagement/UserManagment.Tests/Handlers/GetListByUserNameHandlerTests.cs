using AutoMapper;
using Moq;
using User.Application.Handlers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Entities;
using User.Core.Repositories;

namespace UserManagment.Tests.Handlers
{
    public class GetListByUserNameHandlerTests
    {
        private readonly Mock<IAthorListRepository> _authorListRepositoryMock;
        private readonly GetListByUserNameHandler _handler;

        public GetListByUserNameHandlerTests()
        {
            _authorListRepositoryMock = new Mock<IAthorListRepository>();
            _handler = new GetListByUserNameHandler(_authorListRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthorProductListResponse_WhenProductListFound()
        {
            // Arrange
            var userName = "testUser";

          var expectedResponse = new AuthorProductList("testUser")
          {
                Items = new List<AuthorProductItem>()
                {
                  new AuthorProductItem()
                  {
                     ProductName = "NET 8 DDD, CQRS",
                     ProductId = "102d2149e773f2a3990b47f5",
                     Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
                     ImageFile = "images/products/adidas_shoe-1.png",
                     Price = 29.99m,
                     IsAvailable = true,
                     CreateAt = new DateTime(2030, 1, 1)
                  }
                }
          };
            var productList = new List<AuthorProductItem>()
            {
             new ()
            {
             ProductName = "NET 8 DDD, CQRS",
             ProductId="102d2149e773f2a3990b47f5",
             Description= ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
             ImageFile="images/products/adidas_shoe-1.png",
             Price= 29.99m,
             IsAvailable= true,
             CreateAt=new DateTime(2030, 1, 1)
              },
              new ()
            {
             ProductName = "CQRS",
             ProductId="202d2149e773f2a3990b47f5",
             Description= "CQRS",
             ImageFile="images/products/adidas_shoe-2.png",
             Price= 29.99m,
             IsAvailable= true,
             CreateAt=new DateTime(2031, 1, 1)
              }
            };
            
            _authorListRepositoryMock
                .Setup(repo => repo.GetProductList(userName))
                .ReturnsAsync(expectedResponse);
            
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthorProductItem, AuthorProductListResponse>();
            }).CreateMapper();

            var query = new GetListByUserNameQuery(userName);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedResponse, result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProductListNotFound()
        {
            // Arrange
            var userName = "unknownUser";
            var productList = new AuthorProductList("unknownUser")
            {
                Items = new List<AuthorProductItem>()
                {
                  new AuthorProductItem()
                  {
                     ProductName = "",
                     ProductId = "",
                     Description = "",
                     ImageFile = "",
                     Price = 0m,
                     IsAvailable = false,
                     CreateAt = new DateTime()
                  }
                }
            };

            _authorListRepositoryMock.Setup(repo => repo.GetProductList(userName)).ReturnsAsync(productList);

            var query = new GetListByUserNameQuery(userName);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var userName = "testUser";
            _authorListRepositoryMock.Setup(repo => repo.GetProductList(userName))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetListByUserNameQuery(userName);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
