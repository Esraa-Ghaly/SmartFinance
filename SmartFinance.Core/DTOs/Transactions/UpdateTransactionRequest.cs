using SmartFinance.Core.Models;

namespace SmartFinance.Core.DTOs.Transactions
{
    public class UpdateTransactionRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
    }
}
