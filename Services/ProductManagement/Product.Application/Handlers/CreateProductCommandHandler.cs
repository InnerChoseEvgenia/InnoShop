using FluentValidation;

namespace Product.Application.Handlers
{
    public class CreateProductCommandHandler 
        //(IProductRepository _productRepository) 
        : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _validator = new CreateProductCommandValidator();
        }
        public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
        {
            public CreateProductCommandValidator()
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
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var productEntity = ProductMapper.Mapper.Map<Products>(request);
            if (productEntity is null)
            {
                throw new ApplicationException("There is an issue with mapping while creating new product");
            }
            var newProduct = await _productRepository.CreateProduct(productEntity);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}
