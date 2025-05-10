using Moq;
using Product.Core.Entities;
using Product.Core.Repositories;
using Product.Core.Specs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Product.ApplicationTests1
{
    public static class MockProductService
    {
        public static Mock<IProductRepository> GetProductsMock()
        {
            // database like
            var products = ProductsDataBase();
            var productType = ProductTypeBase();
            var productAuthor = ProductAuthorBase();
            //create mock for interface methods
            var mockProductRepository = new Mock<IProductRepository>();
            var catalogSpecParams = new CatalogSpecParams
            {
                PageIndex = 2,
                PageSize = 20,
                AuthorId = "author123",
                TypeId = "type456",
                Sort = "asc",
                Search = "example search"
            };


            var expectedProducts = new Pagination<Products>
            {
                PageIndex = 2,
                PageSize = 20,
                Count = 2,
                Data = products.ToList()
            };


            mockProductRepository.Setup(repository => repository.CreateProduct(It.IsAny<Products>()))
                .Returns(async (Products product) =>
                {
                    string productId = products.Count > 0 ? products.Last().Id : "202d2149e773f2a3990b47f5";
                    product.Id = productId;
                    products.Add(product);
                    return await Task.FromResult(product);
                });
            mockProductRepository.Setup(repository => repository.UpdateProduct(It.IsAny<Products>()))
                .Returns(async (Products product) =>
                {
                    var productInDb = products
                    .Where(prod => prod.Id == product.Id)
                    .FirstOrDefault();

                    if (productInDb != null)
                    {
                        productInDb.Name = product.Name;
                        productInDb.Description = product.Description;

                        return await Task.FromResult(true);
                    }
                    return await Task.FromResult(false);
                });
            mockProductRepository.Setup(repository => repository.DeleteProduct(It.IsAny<string>()))
                .Returns(async (Products product) =>
                {
                var productInDb = products
                .Where(prod => prod.Id == product.Id)
                .FirstOrDefault();

                   if (productInDb != null)
                   {
                        products.Remove(productInDb);
                        return await Task.FromResult(true);
                    }

                    return await Task.FromResult(false);
                });
            mockProductRepository.Setup(repository=>repository.GetProductsByAuthor(It.IsAny<string>()))
                 .Returns(async (Products product) =>
                   {
                       var productByAuthor = products
                      .Where(prod => prod.Author == product.Author)
                      .FirstOrDefault();

                       if(productByAuthor != null)
                       {
                           return await Task.FromResult(true);
                       }
                       return await Task.FromResult(false);
                   });

            mockProductRepository.Setup(repository => repository.GetProductsByName(It.IsAny<string>()))
                .Returns(async (Products product) =>
                {
                    var productByName = products
                    .Where(prod => prod.Types == product.Types)
                    .FirstOrDefault();

                    if (productByName != null)
                    {
                        return await Task.FromResult(true);
                    }
                    return await Task.FromResult(false);
                });

            mockProductRepository.Setup(repository => repository.GetProductById(It.IsAny<string>()))
                .Returns(async (Products product) =>
                {
                    var productInDb = products
                    .Where(prod => prod.Id == product.Id)
                    .FirstOrDefault();

                    if (productInDb != null)
                    {
                        return await Task.FromResult(true);
                    }

                    return await Task.FromResult(false);
                });
            mockProductRepository.Setup(repository => repository.GetProducts(It.IsAny<CatalogSpecParams>()))
                .Returns(async () =>
                {

                    if (products.Any())

                    {
                        return await Task.FromResult(expectedProducts);
                    }
                    return await Task.FromResult(new Pagination<Products> { 
                        PageIndex = 0,
                        PageSize = 0,
                        Count = 0,
                        Data = Array.Empty<Products>()
                    });
                });

            return mockProductRepository;

        }

        private static List<ProductType> ProductTypeBase()
        {
            return new List<ProductType>()
            {
                new()
                {
                  Id="83ca5d6d958e43ee1cd375fe",
                  Name="Microservicesssss"
                },
                new()
                {
                  Id="93ca5d6d958e43ee1cd375fe",
                  Name="Microservicessssss"
                }
            };
        }
        private static List<ProductAuthor> ProductAuthorBase()
        {
            return new List<ProductAuthor>()
            {
                new()
                {
                  Id="83ca5e4c455900b990b43bc1",
                  Name= "Mehmet"
                },
                new()
                {
                  Id="93ca5e4c455900b990b43bc1",
                  Name= "Kristina"
                }
            };
        }
        private static List<Products> ProductsDataBase()
        {
            return new List<Products>()
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
              },
              new ()
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
             CreateAt=new DateTime(2031, 1, 1)
              }
            };
        }

    }
}
