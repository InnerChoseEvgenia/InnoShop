using FluentValidation;

namespace Product.Application.Handlers
{
    public class UpdateProductCommandHandler (IProductRepository _productRepository)
        : IRequestHandler<UpdateProductCommand, bool>
    {
        //private readonly IProductRepository _productRepository;

        //public UpdateProductCommandHandler(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}
        public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
        {
            public UpdateProductCommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
                RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
                RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
                RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
                RuleFor(x => x.Types).NotEmpty().WithMessage("Types is required");
                //RuleFor(x => x.CreateAt).GreaterThan(expression:).WithMessage("Price must be greater than 0");
            }
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _productRepository.UpdateProduct(new Products

            {
                Id = request.Id,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Name = request.Name,
                Price = request.Price,
                Summary = request.Summary,
                Author = request.Author,
                Types = request.Types,
                IsAvailable= request.IsAvailable,
                CreateAt= request.CreateAt
            });
            return productEntity;
            // changed it during the tests - was return true
        }
    }
}
