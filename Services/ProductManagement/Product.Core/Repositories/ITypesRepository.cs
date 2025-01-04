namespace Product.Core.Repositories
{
    public interface ITypesRepository
    {
        Task<IEnumerable<ProductType>> GetAllTypes();
    }
}
