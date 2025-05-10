using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;

namespace ProductIntegrationTests
{
    public class ProductIntegrationTes : IDisposable
    {

        private readonly ProductContext _context;
        private readonly ProductRepository _repository;

        //public ProductIntegrationTes()
        //{
        //    var options = new DbContextOptionsBuilder<ProductContext()
        //       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //       .Options;

        //    _context = new ProductContext((Microsoft.Extensions.Configuration.IConfiguration)options);
        //    _repository = new ProductRepository(_context);
        //}

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}