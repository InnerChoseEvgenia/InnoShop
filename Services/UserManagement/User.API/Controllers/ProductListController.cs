using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using User.Application.Commands;
using User.Application.Queries;
using User.Application.Responses;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        public readonly IMediator _mediator;
        private readonly ILogger<ProductListController> _logger;

        public ProductListController(IMediator mediator,  ILogger<ProductListController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetProductListByUserName")]
        [ProducesResponseType(typeof(AuthorProductListResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthorProductListResponse>> GetProductList(string userName)
        {
            var query = new GetListByUserNameQuery(userName);
            var list = await _mediator.Send(query);
            return Ok(list);
        }

        [HttpPost("CreateProductList")]
        [ProducesResponseType(typeof(AuthorProductListResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthorProductListResponse>> CreateAuthorList([FromBody] CreateAuthorListCommand createAuthorListCommand)
        {
            var list = await _mediator.Send(createAuthorListCommand);
            return Ok(list);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteProductListByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductList(string userName)
        {
            var cmd = new DeleteProductListByUserNameCommand(userName);
            return Ok(await _mediator.Send(cmd));
        }

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        //{
        //    //Get the existing basket with username
        //    var query = new GetListByUserNameQuery(basketCheckout.UserName);
        //    var basket = await _mediator.Send(query);
        //    if (basket == null)
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
