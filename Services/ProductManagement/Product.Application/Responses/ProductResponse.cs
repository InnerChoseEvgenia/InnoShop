using Newtonsoft.Json;

namespace Product.Application.Responses
{
    public class ProductResponse
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }
        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Summary")]
        public string Summary { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("ImageFile")]
        public string ImageFile { get; set; }
        [JsonProperty("Author")]
        public ProductAuthor Author { get; set; }
        [JsonProperty("Types")]
        public ProductType Types { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }
        [JsonProperty("CreateAt")]
        public DateTime CreateAt { get; set; }
    }
}
