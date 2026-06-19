namespace SmartFinance.Core.DTOs.Transactions
{
    public class AddTransactionRequest
    {
        public decimal Amount { get; set; }

        // ✅ الفئة مرتبطة بالـ CategoryId
        public int CategoryId { get; set; }

        // ✅ خاصية التاريخ
        public DateTime Date { get; set; }

        // ✅ نوع المعاملة (دخل أو مصروف)
        public string Type { get; set; } = string.Empty;

        // ✅ وصف المعاملة
        public string Description { get; set; } = string.Empty;

        // ✅ ربط المعاملة بالمستخدم
        public int UserId { get; set; }
    }
}
