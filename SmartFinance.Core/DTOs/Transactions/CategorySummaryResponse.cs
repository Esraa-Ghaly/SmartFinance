namespace SmartFinance.Core.DTOs.Transactions
{
    public class CategorySummaryResponse
    {
        public string Category { get; set; } = string.Empty;   // ✅ خاصية التصنيف
        public decimal TotalAmount { get; set; }               // ✅ خاصية المجموع
    }
}
