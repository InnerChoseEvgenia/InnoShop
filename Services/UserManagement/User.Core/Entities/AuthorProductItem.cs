﻿namespace User.Core.Entities
{
    public class AuthorProductItem
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
      
        //public string Types { get; set; } 
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
