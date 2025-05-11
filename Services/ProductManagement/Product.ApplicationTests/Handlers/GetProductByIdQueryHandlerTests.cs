using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product.Application.Handlers;
using Moq;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Entities;
using Product.Core.Repositories;
using Xunit;


namespace Product.Application.Handlers.Tests;

[TestClass()]
public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _handler = new GetProductByIdQueryHandler(_mockProductRepository.Object);
    }


    [Fact]
    public async Task Handle_ExistingProductId_ReturnsProductResponse()
    {
        // Arrange
        var productId = "202d2149e773f2a3990b47f5"; // or any valid id
        var product = new Products // Создаем пример продукта
        {
            Id = "202d2149e773f2a3990b47f5",
            Name = "NET 8 DDD, CQRS",
            Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Price = 29.99m,
            ImageFile = "images/products/adidas_shoe-1.png",
            Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Types = new ProductType()
            {
                Id = "63ca5d6d958e43ee1cd375fe",
                Name = "Microservices"
            },
            Author = new ProductAuthor()
            {
                Id = "63ca5e4c455900b990b43bc1",
                Name = "Mehmet Ozkaya"
            },
            IsAvailable = true,
            CreateAt = new DateTime(2030, 1, 1)
        };

        _mockProductRepository.Setup(repo => repo.GetProductById(productId))
            .ReturnsAsync(product); // Настраиваем мок для возврата продукта

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Xunit.Assert.NotNull(result); // Проверяем, что результат не равен null
        Xunit.Assert.Equal(productId, result.Id); // Проверяем, что ID совпадает
        Xunit.Assert.Equal(product.Name, result.Name); // Проверяем имя
    }

    [Fact]
    public async Task Handle_NonExistingProductId_ReturnsNull()
    {
        // Arrange
        var productId = "202d2149e773f2a3990b47f5"; // or any valid id
        var product = new Products // Создаем пример продукта
        {
            Id = "202d2149e773f2a3990b47f5",
            Name = "NET 8 DDD, CQRS",
            Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Price = 29.99m,
            ImageFile = "images/products/adidas_shoe-1.png",
            Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
            Types = new ProductType()
            {
                Id = "63ca5d6d958e43ee1cd375fe",
                Name = "Microservices"
            },
            Author = new ProductAuthor()
            {
                Id = "63ca5e4c455900b990b43bc1",
                Name = "Mehmet Ozkaya"
            },
            IsAvailable = true,
            CreateAt = new DateTime(2030, 1, 1)
        };
        _mockProductRepository.Setup(repo => repo.GetProductById(productId))
            .ReturnsAsync((Products)null); // Настраиваем мок для возврата null

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Xunit.Assert.Null(result); // Проверяем, что результат равен null
    }

    private static List<ProductResponse> ProductsDataBase()
    {
        return new List<ProductResponse>()
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
              }
            };
    }

    
}