namespace Product.Core.Entities
{
    public class Products : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public ProductAuthor Author { get; set; }
        public ProductType Types { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
