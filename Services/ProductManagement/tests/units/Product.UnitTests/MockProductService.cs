using Moq;
using Product.Core.Entities;
using Product.Core.Repositories;
using System.Xml.Linq;
using ZstdSharp.Unsafe;


namespace Product.UnitTests
{
    public static class MockProductService
    {
        public static Mock<IProductRepository> GetProductsMock() 
        {
            // database like
            var products = ProductsDataBase();
            //create mock for interface methods
            var mockProductRepository= new Mock<IProductRepository>();

            mockProductRepository.Setup(service => service.CreateProduct(It.IsAny<Products>()))
                .Returns(async (Products product) =>
                {
                   string productId = products.Count >0 ? products.Last().Id : "202d2149e773f2a3990b47f5";
                   product.Id = productId;
                   products.Add(product);
                   return await Task.FromResult(product);
                });
            mockProductRepository.Setup(ser => ser.UpdateProduct(It.IsAny<Products>()))
                .Returns(async (Products product) =>
                {
                    var productInDb = products
                    .Where(prod => prod.Id == product.Id)
                    .FirstOrDefault();

                    if (productInDb == null)
                    {
                        productInDb.Name = product.Name;
                        productInDb.Description = product.Description;

                        return await Task.FromResult(true);
                    }
                    return await Task.FromResult(false);
                });

            return mockProductRepository;

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
              }
            };
        }


    }
}
