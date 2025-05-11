using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Infrastructure.Repositories
{
    public class AthorListRepository : IAthorListRepository
    {
        private readonly IDistributedCache _redisCache;

        public AthorListRepository (IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task<AuthorProductList> GetProductList(string userName)
        {
            var list = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(list))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<AuthorProductList>(list);
        }


        public async Task<AuthorProductList> CreateProductList(AuthorProductList productList)
        {
            await _redisCache.SetStringAsync(productList.UserName, JsonConvert.SerializeObject(productList));
            return await GetProductList(productList.UserName);
        }

        public async Task DeleteProductList(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

    }
}
