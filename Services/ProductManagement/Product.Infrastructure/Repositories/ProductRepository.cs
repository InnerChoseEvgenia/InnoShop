namespace Product.Infrastructure.Repositories
{
    public class ProductRepository (IProductContext _productContext) 
        : IProductRepository, IAuthorRepository, ITypesRepository 
    {
        async Task<Products> IProductRepository.GetProductById(string id)
        {
            return await _productContext
                .Product
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        async Task<Pagination<Products>> IProductRepository.GetProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Products>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.AuthorId))
            {
                var authorFilter = builder.Eq(p => p.Author.Id, catalogSpecParams.AuthorId);
                filter &= authorFilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typeFilter = builder.Eq(p => p.Types.Id, catalogSpecParams.TypeId);
                filter &= typeFilter;
            }
            var totalItems = await _productContext.Product.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParams, filter);
            return new Pagination<Products>(
                catalogSpecParams.PageIndex,
                catalogSpecParams.PageSize,
                (int)totalItems,
                data
            );
        }

        async Task<IEnumerable<Products>> IProductRepository.GetProductsByAuthor(string authorName)
        {
            return await _productContext
                .Product
                .Find(p => p.Author.Name.ToLower() == authorName.ToLower())
                .ToListAsync();
        }

        async Task<IEnumerable<Products>> IProductRepository.GetProductsByName(string name)
        {
            return await _productContext
                .Product
                .Find(p => p.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }
        async Task<Products> IProductRepository.CreateProduct(Products product)
        {
            await _productContext.Product.InsertOneAsync(product);
            return product;
        }

        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await _productContext
                .Product
                .DeleteOneAsync(p => p.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }
        async Task<bool> IProductRepository.UpdateProduct(Products product)
        {
            var updatedProduct = await _productContext
                .Product
                .ReplaceOneAsync(p => p.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
        async Task<IEnumerable<ProductAuthor>> IAuthorRepository.GetAllProductByAuthor()
        {
            return await _productContext
                .Authors
                .Find(author => true)
                .ToListAsync();
        }

        async Task<IEnumerable<ProductType>> ITypesRepository.GetAllTypes()
        {
            return await _productContext
                .Types
                .Find(type => true)
                .ToListAsync();
        }

        private async Task<IReadOnlyList<Products>> DataFilter
            (CatalogSpecParams catalogSpecParams, FilterDefinition<Products> filter)
        {
            var sortDefn = Builders<Products>.Sort.Ascending("Name"); // Default
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch (catalogSpecParams.Sort)
                {
                    case "priceAsc":
                        sortDefn = Builders<Products>.Sort.Ascending(p => p.Price);
                        break;
                    case "priceDesc":
                        sortDefn = Builders<Products>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDefn = Builders<Products>.Sort.Ascending(p => p.Name);
                        break;
                }
            }
            return await _productContext
            .Product
            .Find(filter)
            .Sort(sortDefn)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
        }
    }
}
