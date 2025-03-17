using InnoShop.Web.Models.User;
using Refit;
using System.Net;

namespace InnoShop.Web.Services
{
    //public interface IUserService
    //{
    //    [Get("/user-service/authorProductList/{userName}")]
    //    Task<GetAuthorProductListResponse> GetAuthorProductList(string userName);

    //    [Post("/user-service/authorProductItem")]
    //    Task<CreateAuthorProductItemResponse> CreateAuthorProductItem(CreateAuthorProductItemRequest request);
    //    [Post("/user-service/authorProductItem")]
    //    Task<UpdateAuthorProductItemResponse> UpdateAuthorProductItem(UpdateAuthorProductItemRequest request);

    //    [Delete("/user - service/authorProductList/{userName}")]
    //    Task<DeleteAuthorProductItemResponse> DeleteAuthorProductItem(string productName);

       
    //    public async Task<AuthorProductList> LoadAuthorProductList()
    //    {
    //        // Get AuthorProductList If Not Exist Create New AuthorProductList with Default Logged In User Name: swn
    //        var userName = "swn";
    //        AuthorProductList list;

    //        try
    //        {
    //            var getAuthorProductListResponse = await GetAuthorProductList(userName);
    //            list = getAuthorProductListResponse.List;
    //        }
    //        catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
    //        {
    //            list = new AuthorProductList
    //            {
    //                UserName = userName,
    //                Items = []
    //            };
    //        }

    //        return list;
    //    }
    //}
}
