using AutoMapper;
using FluentAssertions;
using Moq;
using Product.Application.Commands;
using Product.Application.Handlers;
using Product.Core.Entities;
using Product.Core.Repositories;
using Shouldly;

namespace ProductAplicationTests.Handlers
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CreateProductCommandHandler _handler;
        private readonly CreateProductCommand _validCommand;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            _validCommand = new CreateProductCommand
            {

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

            _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        }


        [Fact]
        public async Task Handle_ValidCommand_CreatesProduct()
        {
            // Arrange
            var productEntity = new Products();
            var createdProduct = new Products(); 

            _productRepositoryMock
                .Setup(repo => repo.CreateProduct(It.IsAny<Products>()))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _handler.Handle(_validCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            _productRepositoryMock.Verify(repo => repo.CreateProduct(It.IsAny<Products>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var invalidCommand = new CreateProductCommand(); 

            // Act
            Func<Task> act = async () => await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task Handle_NullMapping_ThrowsApplicationException()
        {
            // Arrange
            var productMapperMock = new Mock<IMapper>();
            productMapperMock
                .Setup(m => m.Map<Products>(It.IsAny<CreateProductCommand>()))
                .Returns((Products)null); 
            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object);

            // Act
            var result = await _handler.Handle(_validCommand, CancellationToken.None);

            // Assert
            result.ShouldBeNull(); 

        }


        //private Mock<IProductRepository> _productRepositoryMock;
        //private CreateProductCommandHandler _handler;
        //[SetUp]
        //public void SetUp()
        //{
        //    _productRepositoryMock = new Mock<IProductRepository>();
        //    _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        //}
        //public CreateProductCommandHandlerTests()
        //{
        //    _productRepositoryMock = MockProductService.GetProductsMock();
        //    _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        //}

        //[Fact]
        //[Theory(DisplayName = "TC1:Create Product Valid Request")]
        //[MemberData(nameof(ProductParamData.GetCreateProductCommand), MemberType = typeof(ProductParamData))]
        ///*public*/ async Task CreateProductCommandHandler_WithValidRequest_CreatesProduct()
        //(CreateProductCommand createProduct)
        //{
        //    // Arrange

        //    _productRepositoryMock.Setup(repo => repo.CreateProduct(It.IsAny<Products>())).Return();


        //    mockEventStore.Setup(e => e.SaveEventAsync(It.IsAny<ProductCreatedEvent>())).Returns(Task.CompletedTask);

        //    var handler = new CreateProductHandler(mockRepo.Object, mockEventStore.Object);
        //    var command = new CreateProductCommand { Name = "New Product", Price = 99.99m };

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.Equal(1, result);
        //    mockRepo.Verify(r => r.CreateAsync(It.IsAny<Product>()), Times.Once);
        //    mockEventStore.Verify(e => e.SaveEventAsync(It.IsAny<ProductCreatedEvent>()), Times.Once);


        //    //Arrage

        //    var handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        //    var product = new Products()
        //    {
        //        Name = "NET 8 DDD, CQRS",
        //        Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Price = 29,
        //        ImageFile = "images/products/adidas_shoe-1.png",
        //        Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Types = new ProductType()
        //        {
        //            Id = "63ca5d6d958e43ee1cd375fe",
        //            Name = "Microservices"
        //        },
        //        Author = new ProductAuthor()
        //        {
        //            Id = "63ca5e4c455900b990b43bc1",
        //            Name = "Mehmet Ozkaya"
        //        },
        //        IsAvailable = true,
        //        CreateAt = new DateTime(2030, 1, 1)
        //    };

        //    _productRepositoryMock.Setup(repo => repo.CreateProduct( new Products()))
        //        .ReturnsAsync(product);

        //    var mapper = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<CreateProductCommandHandler, Products>();
        //    }).CreateMapper();


        //    //Act 
        //    var response = await handler.Handle(new CreateProductCommand(), CancellationToken.None);

        //    //Assert
        //    Assert.NotNull(response);

        //}

        //[Fact]
        //public void Validate_Should_Have_Error_When_Name_Is_Empty()
        //{
        //    // Arrange
        //    var validator = new CreateProductCommandValidator();
        //    var command = new CreateProductCommand
        //    {
        //        Name = "",
        //        Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Price = 29.99m,
        //        ImageFile = "images/products/adidas_shoe-1.png",
        //        Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Types = new ProductType()
        //        {
        //            Id = "63ca5d6d958e43ee1cd375fe",
        //            Name = "Microservices"
        //        },
        //        Author = new ProductAuthor()
        //        {
        //            Id = "63ca5e4c455900b990b43bc1",
        //            Name = "Mehmet Ozkaya"
        //        },
        //        IsAvailable = true,
        //        CreateAt = new DateTime(2030, 1, 1)
        //    };

        //    // Act
        //    var result = validator.TestValidate(command);

        //    // Assert
        //    result.ShouldHaveValidationErrorFor(x => x.Name);
        //}

        //[Fact]
        //public async Task Handle_Should_Create_Product_When_Valid()
        //{
        //    // Arrange
        //    var command = new CreateProductCommand
        //    {
        //        Name = "NET 8 DDD, CQRS",
        //        Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Price = 29.99m,
        //        ImageFile = "images/products/adidas_shoe-1.png",
        //        Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Types = new ProductType()
        //        {
        //            Id = "63ca5d6d958e43ee1cd375fe",
        //            Name = "Microservices"
        //        },
        //        Author = new ProductAuthor()
        //        {
        //            Id = "63ca5e4c455900b990b43bc1",
        //            Name = "Mehmet Ozkaya"
        //        },
        //        IsAvailable = true,
        //        CreateAt = new DateTime(2030, 1, 1)
        //    };

        //    var productEntity = new Products(); // Assume this maps from command
        //    var createdProduct = new Products(); // Mock the created product response

        //    // Setup mock to return createdProduct when CreateProduct is called
        //    _productRepositoryMock.Setup(repo => repo.CreateProduct(It.IsAny<Products>())).ReturnsAsync(createdProduct);

        //    // Act
        //    //var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    //Xunit.Assert.NotNull(result);
        //    _productRepositoryMock.Verify(repo => repo.CreateProduct(It.Is<Products>(p => p.Name == command.Name)), Times.Once);
        //}

        //[Fact]
        //public void Validate_Should_Have_Error_When_Price_Is_Zero()
        //{
        //    // Arrange
        //    var validator = new CreateProductCommandValidator();
        //    var command = new CreateProductCommand
        //    {
        //        Name = "NET 8 DDD, CQRS",
        //        Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Price = 29.99m,
        //        ImageFile = "images/products/adidas_shoe-1.png",
        //        Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Types = new ProductType()
        //        {
        //            Id = "63ca5d6d958e43ee1cd375fe",
        //            Name = "Microservices"
        //        },
        //        Author = new ProductAuthor()
        //        {
        //            Id = "63ca5e4c455900b990b43bc1",
        //            Name = "Mehmet Ozkaya"
        //        },
        //        IsAvailable = true,
        //        CreateAt = new DateTime(2030, 1, 1)
        //    };

        //    // Act
        //    var result = validator.TestValidate(command);

        //    // Assert
        //    result.ShouldHaveValidationErrorFor(x => x.Price);
        //}

        //[Fact]
        //public async Task Handle_Should_Throw_Exception_If_Mapping_Fails()
        //{
        //    // Arrange
        //    var command = new CreateProductCommand
        //    {
        //        // No properties set intentionally to cause a mapping failure
        //        Name = "NET 8 DDD, CQRS",
        //        Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Price = 29.99m,
        //        ImageFile = "images/products/adidas_shoe-1.png",
        //        Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
        //        Types = new ProductType()
        //        {
        //            Id = "63ca5d6d958e43ee1cd375fe",
        //            Name = "Microservices"
        //        },
        //        Author = new ProductAuthor()
        //        {
        //            Id = "63ca5e4c455900b990b43bc1",
        //            Name = "Mehmet Ozkaya"
        //        },
        //        IsAvailable = true,
        //        CreateAt = new DateTime(2030, 1, 1)
        //    };

        //    // Act & Assert
        //    Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(command, CancellationToken.None));
        //}
    }
}

//    [Fact(DisplayName = "TC2:Create Product InValid Request")]
//    public async Task CreateProductCommandHandler_WithInvalidRequest_DoesNotCreateProduct()
//    {
//        //Arrage
//        var mockMapper = new Mock<IMapper>();
//        var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, mockMapper.Object);

//        //Act 
//        var response = await handler.Handle(new CreateProductCommand { CreateProduct = null }, CancellationToken.None);

//        //Assert
//        response.IsSuccessful.ShouldBeFalse();
//    }

//var mockMapper = new Mock<IMapper>();
//var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, mockMapper.Object);
//_productRepositoryMock.Setup(repo => repo.CreateProduct(product))
//   .Returns(new Products()
//   {
//       Name = "NET 8 DDD, CQRS",
//       Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
//       Price = 29.99m,
//       ImageFile = "images/products/adidas_shoe-1.png",
//       Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
//       Types = new ProductType()
//       {
//           Id = "63ca5d6d958e43ee1cd375fe",
//           Name = "Microservices"
//       },
//       Author = new ProductAuthor()
//       {
//           Id = "63ca5e4c455900b990b43bc1",
//           Name = "Mehmet Ozkaya"
//       },
//       IsAvailable = true,
//       CreateAt = new DateTime(2030, 1, 1)
//   });

//var query = new CreateProductCommand();

//// Act
//var result = await _handler.Handle(query, CancellationToken.None);
//mockMapper.Setup(mapper => mapper.Map<Products>(createProduct))
//  .Returns(new Products()
//  {
//      Name = "NET 8 DDD, CQRS",
//      Description = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
//      Price = 29.99m,
//      ImageFile = "images/products/adidas_shoe-1.png",
//      Summary = ".NET 8 Microservices: DDD, CQRS, Vertical/Clean Architecture",
//      Types = new ProductType()
//      {
//          Id = "63ca5d6d958e43ee1cd375fe",
//          Name = "Microservices"
//      },
//      Author = new ProductAuthor()
//      {
//          Id = "63ca5e4c455900b990b43bc1",
//          Name = "Mehmet Ozkaya"
//      },
//      IsAvailable = true,
//      CreateAt = new DateTime(2030, 1, 1)
//  });

