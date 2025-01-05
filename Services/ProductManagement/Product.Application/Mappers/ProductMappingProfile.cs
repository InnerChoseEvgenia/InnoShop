namespace Product.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductAuthor, AuthorResponse>().ReverseMap();
            CreateMap<Products, ProductResponse>().ReverseMap();
            CreateMap<ProductType, TypesResponse>().ReverseMap();
            CreateMap<Products, CreateProductCommand>().ReverseMap();
            CreateMap<Pagination<Products>, Pagination<ProductResponse>>().ReverseMap();
        }
    }
}
