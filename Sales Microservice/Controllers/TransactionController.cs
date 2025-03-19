using Microsoft.AspNetCore.Mvc;
using Sales_Microservice.Services.Interfaces;

namespace Sales_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var sales = await _transactionService.GetAllTransactionsAsync();
                return Ok(sales); // Return HTTP 200 with data
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }

        [HttpGet("GetTransactionReports")]
        public async Task<IActionResult> GetSalesReports()
        {
            try
            {
                var report = await _transactionService.GetAllReportsAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }

        }

        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionAsync(transactionId);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }
    }
}
