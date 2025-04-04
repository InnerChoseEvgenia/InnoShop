﻿namespace Product.Application.Handlers
{
    public class DeleteProductByIdCommandHandler (IProductRepository _productRepository) 
        : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        //private readonly IProductRepository _productRepository;

        //public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}
        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProduct(request.Id);
        }
    }
}
