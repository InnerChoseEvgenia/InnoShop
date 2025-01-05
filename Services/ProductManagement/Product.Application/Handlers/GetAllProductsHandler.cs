namespace Product.Application.Handlers
{
    public class GetAllProductsHandler (IProductRepository _productRepository)
        : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        //private readonly IProductRepository _productRepository;

        //public GetAllProductsHandler(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProducts(request.CatalogSpecParams);
            var productResposeList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productResposeList;
        }
    }
}
