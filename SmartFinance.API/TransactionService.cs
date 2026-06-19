using Microsoft.EntityFrameworkCore;
using SmartFinance.Infrastructure.Data;
using SmartFinance.Core.Models;
using SmartFinance.Core.DTOs.Transactions;

namespace SmartFinance.Core.Services
{
    public class TransactionService
    {
        private readonly SmartFinanceDbContext _context;

        public TransactionService(SmartFinanceDbContext context)
        {
            _context = context;
        }

        // إضافة معاملة جديدة
        public async Task<TransactionResponse> AddTransactionAsync(AddTransactionRequest request)
        {
            var transaction = new Transaction
            {
                Amount = request.Amount,
                Description = request.Description,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                Date = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new TransactionResponse
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                CategoryName = (await _context.Categories.FindAsync(transaction.CategoryId))?.Name ?? string.Empty,
                Date = transaction.Date
            };
        }

        // جلب معاملات مستخدم معين
        public async Task<List<TransactionResponse>> GetTransactionsByUserAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    CategoryName = t.Category.Name,
                    Date = t.Date
                })
                .ToListAsync(); // النوع واضح هنا لأنه TransactionResponse
        }

        // ملخص المصروفات حسب التصنيف
        public async Task<List<CategorySummaryResponse>> GetCategorySummaryAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategorySummaryResponse
                {
                    CategoryName = g.Key,
                    TotalSpent = g.Sum(t => t.Amount)
                })
                .ToListAsync(); // النوع واضح هنا لأنه CategorySummaryResponse
        }
    }
}
