using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Specs;
using System.Net;

namespace Product.API.Controllers
{
      public class ProductController (IMediator _mediator, ILogger<ProductController> _logger)
        : ApiController
    {
        //private readonly IMediator _mediator;
        //private readonly ILogger<CatalogController> _logger;

        //public CatalogController(IMediator mediator, ILogger<CatalogController> logger)
        //{
        //    _mediator = mediator;
        //    _logger = logger;
        //}

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await _mediator.Send(query);
            _logger.LogInformation($"Product with {productName} fetched");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllAuthor")]
        [ProducesResponseType(typeof(IList<AuthorResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<AuthorResponse>>> GetAllAuthor()
        {
            var query = new GetAllAuthorQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{author}", Name = "GetProductsByAuthorName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductsByAuthorName(string author)
        {
            var query = new GetProductByAuthorQuery(author);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send<ProductResponse>(productCommand);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(string id)
        {
            var command = new DeleteProductByIdCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
