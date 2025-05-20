using Microsoft.AspNetCore.Mvc;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Responses;
using StockControlAPI.Hateoas;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService, IStockService stockService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IStockService _stockService = stockService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            var product = _productService.AddProduct(productDto);
            var response = ProductHateoasBuilder.Build(product, Url);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts([FromQuery] bool isActive = true, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var products = _productService.GetProducts(isActive, skip, take);
            var response = ProductHateoasBuilder.BuildList(products, Url);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            var response = ProductHateoasBuilder.Build(product, Url);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            var updatedProduct = _productService.UpdateProduct(id, productDto);
            var response = ProductHateoasBuilder.Build(updatedProduct, Url);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Product removed successfully."));
        }

        [HttpPost("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddStock([FromRoute] int id, [FromBody] int quantity)
        {
            var stock = _stockService.AddStock(new StockDto(id, quantity));
            return Ok(stock);
        }

        [HttpDelete("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteStock(int id)
        {
            _stockService.DeleteStock(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Product removed successfully."));
        }
    }
}