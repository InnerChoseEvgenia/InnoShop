using Mongo2Go;
using MongoDB.Driver;



namespace Product.Integration.Tests
{
    public class MongoDbFixture : IDisposable
    //: WebApplicationFactory<Program>
    {
        public MongoDbRunner Runner { get; private set; }
        public MongoClient Client { get; private set; }



        public MongoDbFixture()
        {
            Runner = MongoDbRunner.Start();
            Client = new MongoClient(Runner.ConnectionString);
        }

        public void Dispose()
        {
            Runner.Dispose();
        }


        ////    private readonly IMongoDatabase _database;

        ////    protected override void ConfigureWebHost(IWebHostBuilder builder)
        ////    {
        ////        builder.ConfigureTestServices(services =>
        ////        {
        ////            var descriptor = services
        ////            .SingleOrDefault(s => s.ServiceType == typeof(IMongoClient));

        ////            if (descriptor != null)
        ////            {
        ////                services.Remove(descriptor);
        ////            }

        ////            services.AddDbContext<ProductContext>(sp =>
        ////            {

        ////                var connectionString = "mongodb://localhost:27017"; // Измените на вашу строку подключения
        ////                return new MongoClient(connectionString);
        ////            });

        ////            // Дополнительно вы можете инициализировать базу данных с фиктивными данными, если это необходимо
        ////            services.AddScoped<IMongoDbContext, MongoDbContext>();
        ////            // Или инициализация коллекций и данных прямо в тестах, если это нужно.
        ////        });
        ////    }
        ////}

        ////// Примерный интерфейс контекста MongoDB
        ////public interface IMongoDbContext
        ////{
        ////    IMongoCollection<ProductContext> Products { get; }
        ////}

        ////// Примерный реализация контекста MongoDB
        ////public class MongoDbContext : IMongoDbContext
        ////{
        ////    private readonly IMongoDatabase _database;

        ////    public MongoDbContext(IMongoClient mongoClient)
        ////    {
        ////        _database = mongoClient.GetDatabase("TestDatabase"); // Замените на ваше имя базы данных
        ////    }

        ////    public IMongoCollection<ProductContext> Products => _database.GetCollection<ProductContext>("Products");
        ////}
    }
}

