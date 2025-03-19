using Sales_Microservice.dto;
using Microsoft.AspNetCore.Mvc;
using Sales_Microservice.Services.Interfaces;

namespace Sales_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("GetInventory")]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryAsync();
                return Ok(inventory); // Return HTTP 200 with data
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }

        [HttpGet("GetInventoryById")]
        public async Task<IActionResult> GetInventoryById(int inventoryId)
        {
            try
            {
                var inventory = await _inventoryService.GetItemByIdAsync(inventoryId);
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }

        [HttpPost("ReduceInventory")]
        public async Task<IActionResult> ReduceInventory(ReduceInventoryDTO reduceInventoryDTO)
        {
            try
            {
                var inventory = await _inventoryService.ReduceInventoryAsync(reduceInventoryDTO);
                return Ok(inventory);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }
    }
}
