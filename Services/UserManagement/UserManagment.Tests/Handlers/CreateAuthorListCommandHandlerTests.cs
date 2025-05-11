using AutoMapper;
using Moq;
using User.Application.Commands;
using User.Application.Handlers;
using User.Application.Responses;
using User.Core.Entities;
using User.Core.Repositories;

namespace UserManagment.Tests.Handlers
{
    public class CreateAuthorListCommandHandlerTests
    {
        private readonly Mock<IAthorListRepository> _athorListRepositoryMock;
        private readonly CreateAuthorListCommandHandler _handler;

        public CreateAuthorListCommandHandlerTests()
        {
            _athorListRepositoryMock = new Mock<IAthorListRepository>();
            _handler = new CreateAuthorListCommandHandler(_athorListRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthorProductListResponse_WhenCreateProductListIsSuccessful()
        {
            // Arrange
            var command = new CreateAuthorListCommand(
                username: "TestUser",
                items: new List<AuthorProductItem>
                {
                  new AuthorProductItem
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
            );

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthorProductItem, AuthorProductItemResponse>(); 
                cfg.CreateMap<AuthorProductList, AuthorProductListResponse>(); 
            }).CreateMapper();

            var authorProductItemsResponse = mapper.Map<List<AuthorProductItemResponse>>(command.Items);
            var authorProductList = new AuthorProductList
            {
                UserName = command.UserName,
                Items = command.Items
            };

            var expectedResponse = new AuthorProductListResponse
            {
                UserName = command.UserName,
                Items = authorProductItemsResponse 
            };

            _athorListRepositoryMock
                .Setup(repo => repo.CreateProductList(It.IsAny<AuthorProductList>()))
                .ReturnsAsync(authorProductList);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.UserName, result.UserName);
            Assert.Equal(expectedResponse.Items.Count, result.Items.Count);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCreateProductListFails()
        {
            // Arrange
            var command = new CreateAuthorListCommand(
                username: "TestUser",
                items: new List<AuthorProductItem>
                {
                  new AuthorProductItem
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
            );

            _athorListRepositoryMock
                .Setup(repo => repo.CreateProductList(It.IsAny<AuthorProductList>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
