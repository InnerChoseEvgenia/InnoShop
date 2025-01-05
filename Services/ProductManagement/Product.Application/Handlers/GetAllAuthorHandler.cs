namespace Product.Application.Handlers
{
    public class GetAllAuthorHandler (IAuthorRepository _authorRepository) 
        : IRequestHandler<GetAllAuthorQuery, IList<AuthorResponse>>
    {
        //private readonly IAuthorRepository _authorRepository;

        //public GetAllAuthorHandler(IAuthorRepository authorRepository)
        //{
        //    _authorRepository = authorRepository;
        //}
        public async Task<IList<AuthorResponse>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            var authorList = await _authorRepository.GetAllProductByAuthor();
            var authorResponseList = ProductMapper.Mapper.Map<IList<ProductAuthor>, IList<AuthorResponse>>(authorList.ToList());
            return authorResponseList;
        }
    }
}

