using SmartFinance.Core.DTOs.Transactions;
using SmartFinance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartFinance.Services
{
    public class ReportService
    {
        private readonly SmartFinanceDbContext _context;

        public ReportService(SmartFinanceDbContext context)
        {
            _context = context;
        }

        // ✅ الميثود اللي بينادي عليها الـ Controller
        public async Task<List<CategorySummaryResponse>> GenerateReportAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategorySummaryResponse
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToListAsync();
        }
    }
}
