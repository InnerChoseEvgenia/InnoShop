using Product.Core.Specs;

namespace Product.Application.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
    {
        public CatalogSpecParams CatalogSpecParams { get; set; }
        public GetAllProductsQuery(CatalogSpecParams catalogSpecParams)
        {
            CatalogSpecParams = catalogSpecParams;
        }

    }
}
