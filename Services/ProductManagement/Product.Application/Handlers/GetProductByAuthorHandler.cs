namespace Product.Application.Handlers
{
    public class GetProductByAuthorHandler (IProductRepository _productRepository) 
        : IRequestHandler<GetProductByAuthorQuery, IList<ProductResponse>>
    {
        //private readonly IProductRepository _productRepository;

        //public GetProductByAuthorHandler(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}
        public async Task<IList<ProductResponse>> Handle(GetProductByAuthorQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductsByAuthor(request.AuthorName);
            var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
