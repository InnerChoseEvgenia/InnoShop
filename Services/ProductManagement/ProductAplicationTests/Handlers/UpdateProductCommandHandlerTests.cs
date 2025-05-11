using Moq;
using Product.Application.Commands;
using Product.Application.Handlers;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = "202d2149e773f2a3990b47f5",
                Name = "Test Product",
                Description = "Test Description",
                ImageFile = "test-image.jpg",
                Price = 10.99m,
                Summary = "Test Summary",
                Author = new ProductAuthor()
                {
                    Id = "63ca5e4c455900b990b43bc1",
                    Name = "Mehmet Ozkaya"
                },
                Types = new ProductType()
                {
                    Id = "63ca5d6d958e43ee1cd375fe",
                    Name = "Microservices"
                },
                IsAvailable = true,
                CreateAt = DateTime.Now               
            };

            _productRepositoryMock.Setup(repo => repo.UpdateProduct(It.IsAny<Products>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(repo => repo.UpdateProduct(It.IsAny<Products>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFalse()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = "",
                Name = "",
                Description = "Test Description",
                ImageFile = "test-image.jpg",
                Price = 10.99m,
                Summary = "Test Summary",
                Author = new ProductAuthor()
                {
                    Id = "63ca5e4c455900b990b43bc1",
                    Name = "Mehmet Ozkaya"
                },
                Types = new ProductType()
                {
                    Id = "63ca5d6d958e43ee1cd375fe",
                    Name = "Microservices"
                },
                IsAvailable = true,
                CreateAt = DateTime.Now
            };

            var validator = new UpdateProductCommandHandler.UpdateProductCommandValidator();
            var validationResult =  validator.Validate(command);

            // Assert 
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task Handle_ProductUpdateFails_ReturnsFalse()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = "202d2149e773f2a3990b47f5",
                Name = "Test Product",
                Description = "Test Description",
                ImageFile = "test-image.jpg",
                Price = 10.99m,
                Summary = "Test Summary",
                Author = new ProductAuthor()
                {
                    Id = "63ca5e4c455900b990b43bc1",
                    Name = "Mehmet Ozkaya"
                },
                Types = new ProductType()
                {
                    Id = "63ca5d6d958e43ee1cd375fe",
                    Name = "Microservices"
                },
                IsAvailable = true,
                CreateAt = DateTime.Now
            };

            _productRepositoryMock.Setup(repo => repo.UpdateProduct(It.IsAny<Products>()))
                .ReturnsAsync(false);  

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _productRepositoryMock.Verify(repo => repo.UpdateProduct(It.IsAny<Products>()), Times.Once);
        }
    }
}
