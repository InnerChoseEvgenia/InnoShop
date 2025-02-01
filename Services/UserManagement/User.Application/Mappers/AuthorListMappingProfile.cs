using AutoMapper;
using User.Application.Responses;
using User.Core.Entities;

namespace User.Application.Mappers
{
    public class AuthorListMappingProfile : Profile
    {
        public AuthorListMappingProfile()
        {
            CreateMap<AuthorProductList, AuthorProductListResponse>().ReverseMap();
            CreateMap<AuthorProductItem, AuthorProductItemResponse>().ReverseMap();
        }
    }
}
