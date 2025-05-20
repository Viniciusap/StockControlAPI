using Microsoft.AspNetCore.Mvc;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Responses;
using StockControlAPI.Service.Interfaces;

namespace StockControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController(IStockService stockService) : ControllerBase
    {
        private readonly IStockService _stockService = stockService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddStock([FromBody] StockDto stockDto)
        {
            var stock = _stockService.AddStock(stockDto);
            return Ok(stock);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteStock(int id)
        {
            _stockService.DeleteStock(id);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Product removed successfully."));
        }
    }
}