using AutoMapper;

namespace User.Application.Mappers
{
    public static class AuthorListMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
      {
          var config = new MapperConfiguration(cfg =>
          {
              cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
              cfg.AddProfile<AuthorListMappingProfile>();
          });
          var mapper = config.CreateMapper();
          return mapper;
      });
        public static IMapper Mapper => Lazy.Value;
    }
}
