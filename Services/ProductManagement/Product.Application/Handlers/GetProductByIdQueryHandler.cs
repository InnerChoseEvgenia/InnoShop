namespace Product.Application.Handlers
{
    public class GetProductByIdQueryHandler (IProductRepository _productRepository)
        : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        //private readonly IProductRepository _productRepository;

        //public GetProductByIdQueryHandler(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id);
            var productRespose = ProductMapper.Mapper.Map<ProductResponse>(product);
            return productRespose;
        }
    }
}
