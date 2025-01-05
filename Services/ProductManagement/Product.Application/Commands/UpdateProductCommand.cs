namespace Product.Application.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public ProductAuthor Author { get; set; }
        public ProductType Types { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
