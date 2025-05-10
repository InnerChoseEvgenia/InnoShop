using Moq;
using Product.Application.Commands;
using Product.Application.Handlers;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    //[TestFixture]
    public class DeleteProductByIdCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private DeleteProductByIdCommandHandler _handler;

        //[SetUp]
        //public void SetUp()
        //{
        //    _productRepositoryMock = new Mock<IProductRepository>();
        //    _handler = new DeleteProductByIdCommandHandler(_productRepositoryMock.Object);
        //}
        public DeleteProductByIdCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductByIdCommandHandler(_productRepositoryMock.Object);
        }

        //[Test]
        [Fact]
        public async Task Handle_ProductExists_ReturnsTrue()
        {
            // Arrange
            var idDeletedProduct = "202d2149e773f2a3990b47f5";
            var command = new DeleteProductByIdCommand(idDeletedProduct);
            _productRepositoryMock.Setup(repo => repo.DeleteProduct(command.Id)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _productRepositoryMock.Verify(repo => repo.DeleteProduct(command.Id), Times.Once);
        }

        //[Test]
        [Fact]
        public async Task Handle_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var idDeletedProduct = "202d2149e773f2a3990b47f5";
            var command = new DeleteProductByIdCommand(idDeletedProduct);
            _productRepositoryMock.Setup(repo => repo.DeleteProduct(command.Id)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _productRepositoryMock.Verify(repo => repo.DeleteProduct(command.Id), Times.Once);
        }
    }
}
