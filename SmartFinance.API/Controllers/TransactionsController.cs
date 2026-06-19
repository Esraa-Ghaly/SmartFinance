using Microsoft.AspNetCore.Mvc;
using SmartFinance.Core.Services;
using SmartFinance.Core.DTOs.Transactions;

namespace SmartFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionRequest request)
            => Ok(await _transactionService.AddTransactionAsync(request));

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserTransactions(int userId)
            => Ok(await _transactionService.GetTransactionsByUserAsync(userId));

        [HttpGet("categories-summary/{userId}")]
        public async Task<IActionResult> GetCategorySummary(int userId)
            => Ok(await _transactionService.GetCategorySummaryAsync(userId));
    }
}
