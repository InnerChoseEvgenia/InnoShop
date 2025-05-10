using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    public class GetAllAuthorHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly GetAllAuthorHandler _handler;

        public GetAllAuthorHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _handler = new GetAllAuthorHandler(_authorRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthors_WhenAuthorsExists()
        {
            // Arrange
            var authors = new List<ProductAuthor>
        {
            new ProductAuthor 
            {  
                Id="83ca5e4c455900b990b43bc1",
                Name= "Author 1"
            },
            new ProductAuthor 
            { 
                Id="93ca5e4c455900b990b43bc1",
                Name= "Author 2"
            }
        };

            _authorRepositoryMock.Setup(repo => repo.GetAllProductByAuthor())
                .ReturnsAsync(authors);

            var query = new GetAllAuthorQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AuthorResponse>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Author 1", result[0].Name);
            Assert.Equal("Author 2", result[1].Name);
            _authorRepositoryMock.Verify(repo => repo.GetAllProductByAuthor(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoAuthorsExist()
        {
            // Arrange
            _authorRepositoryMock.Setup(repo => repo.GetAllProductByAuthor())
                .ReturnsAsync(new List<ProductAuthor>());

            var query = new GetAllAuthorQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AuthorResponse>>(result);
            Assert.Empty(result);
            _authorRepositoryMock.Verify(repo => repo.GetAllProductByAuthor(), Times.Once);
        }
    }
}
