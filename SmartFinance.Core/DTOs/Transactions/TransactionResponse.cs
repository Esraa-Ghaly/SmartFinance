namespace SmartFinance.Core.DTOs.Transactions
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;   // ✅ بدل CategoryName
        public string Type { get; set; } = string.Empty;       // ✅ خاصية النوع
    }
}
