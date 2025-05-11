using Moq;
using User.Application.Commands;
using User.Application.Handlers;
using User.Core.Repositories;

namespace UserManagment.Tests.Handlers
{
    public class DeleteProductListByUserNameHandlerTests
    {
        private readonly Mock<IAthorListRepository> _authorListRepositoryMock;
        private readonly DeleteProductListByUserNameHandler _handler;

        public DeleteProductListByUserNameHandlerTests()
        {
            _authorListRepositoryMock = new Mock<IAthorListRepository>();
            _handler = new DeleteProductListByUserNameHandler(_authorListRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteProductList_WhenValidUserNameProvided()
        {
            // Arrange
            var command = new DeleteProductListByUserNameCommand ( "testUser" );

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _authorListRepositoryMock.Verify(repo => repo.DeleteProductList(command.UserName), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotCallDeleteProductList_IfCommandIsNull()
        {
            // Arrange
            DeleteProductListByUserNameCommand command = null;

            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));

            // Assert
            _authorListRepositoryMock.Verify(repo => repo.DeleteProductList(It.IsAny<string>()), Times.Never);
        }

    }
}
