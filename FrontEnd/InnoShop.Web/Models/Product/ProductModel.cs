using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace InnoShop.Web.Models.Product
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public ProductAuthor Author { get; set; }
        public ProductType Types { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public class ProductAuthor 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
    }
    public class ProductType

    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; } 
    }

    public class CatalogSpecParams
    {
        private const int MaxPageSize = 70;
        private int _pageSize = 10;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? AuthorId { get; set; }
        public string? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }

        //public bool IsAvailable { get; set; }
        //public DateTime CreateAt { get; set; }
    }
    public class Pagination<T> where T : class
    {
        
        //internal IList<ProductModel> Products;

        //internal readonly IEnumerable<ProductModel> Products;

        public Pagination()
        {

        }
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        //public IEnumerable<T> Products { get; }
        //internal set; } 
        //= new List<ProductModel>();
    }

    public record GetAllProductsQuery(CatalogSpecParams catalogSpecParams) : IRequest<Pagination<Products>> { }
   
    //public record <Pagination<ProductResponse>>
    public record ProductResponse(Pagination<Products> Products);
    //public record GetProductsResponse(Pagination<ProductModel> Products);
    //public record ProductResponse(IList<Pagination<ProductModel>> Products);
    //public record GetAllProductsQuery(CatalogSpecParams catalogSpecParams);
    //public record GetProductsResponse(IEnumerable<ProductModel> Products);
    public record GetProductByAuthorResponse(IEnumerable<ProductAuthor> Products);
    //public record GetProductByTypeResponse(IEnumerable<ProductModel> Products);
    public record GetProductByNameResponse(Products Product);
    public record GetProductByIdResponse(Products Product);
}

