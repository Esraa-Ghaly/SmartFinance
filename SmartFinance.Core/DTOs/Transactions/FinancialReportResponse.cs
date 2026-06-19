using SmartFinance.Core.DTOs.Transactions;

namespace SmartFinance.Core.DTOs.Transactions
{
    public class FinancialReportResponse
    {
        public decimal TotalExpenses { get; set; }
        public decimal DailyAverage { get; set; }
        public List<CategorySummaryResponse> Categories { get; set; } = new();
    }
}
