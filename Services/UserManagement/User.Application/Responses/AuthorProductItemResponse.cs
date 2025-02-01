namespace User.Application.Responses
{
    public class AuthorProductItemResponse
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        //public string Summary { get; set; }
        public string ImageFile { get; set; }
        //public ProductAuthor Author { get; set; }
        //public ProductType Types { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
