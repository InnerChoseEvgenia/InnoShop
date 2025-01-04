namespace Product.Core.Entities
{
    public class ProductAuthor : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
