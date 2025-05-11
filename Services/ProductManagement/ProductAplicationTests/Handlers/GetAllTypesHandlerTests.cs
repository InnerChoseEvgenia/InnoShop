using AutoMapper;
using Moq;
using Product.Application.Handlers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;

namespace ProductAplicationTests.Handlers
{
    public class GetAllTypesHandlerTests
    {

        private Mock<ITypesRepository> _typesRepositoryMock;
        private GetAllTypesHandler _handler;


        //[SetUp]
        //public void Setup()
        public GetAllTypesHandlerTests()
        {
            _typesRepositoryMock = new Mock<ITypesRepository>();
            _handler = new GetAllTypesHandler(_typesRepositoryMock.Object);
        }
        //[Test]
        [Fact]
        public async Task Handle_ShouldReturnTypesResponseList_WhenTypesExist()
        {
            // Arrange
            var typesList = new List<ProductType>
        {
            new ProductType {   Id = "63ca5d6d958e43ee1cd375fe",
                                Name = "Microservices" },
            new ProductType {   Id = "73ca5d6d958e43ee1cd375fe",
                                Name = "Micro" }
        };

            var typesResponseList = new List<TypesResponse>
        {
            new TypesResponse {   Id = "63ca5d6d958e43ee1cd375fe",
                                 Name = "Microservices" },
            new TypesResponse {  Id = "73ca5d6d958e43ee1cd375fe",
                                Name = "Micro"}
        };

            _typesRepositoryMock.Setup(repo => repo.GetAllTypes())
                .ReturnsAsync(typesList);

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductType, TypesResponse>();
            }).CreateMapper();

            _handler = new GetAllTypesHandler(_typesRepositoryMock.Object);
            //var query = new GetAllTypesQuery(typesResponseList);

            // Act
            var result = await _handler.Handle(new GetAllTypesQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
           // Assert.IsType<IList<TypesResponse>>(result);
            Assert.Equal(typesResponseList.Count, result.Count);
        }

        //[Test]
        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoTypesExist()
        {
            // Arrange
            _typesRepositoryMock.Setup(repo => repo.GetAllTypes())
                .ReturnsAsync(new List<ProductType>());

            // Act
            var result = await _handler.Handle(new GetAllTypesQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
