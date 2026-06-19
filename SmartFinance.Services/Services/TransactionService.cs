using SmartFinance.Core.DTOs.Transactions;
using SmartFinance.Core.Interfaces;
using SmartFinance.Core.Models;
using SmartFinance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartFinance.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly SmartFinanceDbContext _context;

        public TransactionService(SmartFinanceDbContext context)
        {
            _context = context;
        }

        // ✅ إضافة مصروف جديد
        public async Task<Transaction> AddTransactionAsync(AddTransactionRequest request)
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Category = request.Category,
                Description = request.Description,
                Date = request.Date,
                Type = TransactionType.Expense
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        // ✅ عرض كل معاملات المستخدم
        public async Task<List<TransactionResponse>> GetTransactionsByUserAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    Category = t.Category,
                    Type = t.Type
                })
                .ToListAsync();
        }

        // ✅ الحصول على معاملة بالـ Id
        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        // ✅ تعديل معاملة
        public async Task<Transaction?> UpdateTransactionAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return null;

            transaction.Amount = request.Amount;
            transaction.Description = request.Description;
            transaction.Date = request.Date;
            transaction.Category = request.Category;
            transaction.Type = request.Type;

            await _context.SaveChangesAsync();
            return transaction;
        }

        // ✅ حذف معاملة
        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ عرض آخر المعاملات
        public async Task<List<TransactionResponse>> GetRecentTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Take(5)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    Category = t.Category,
                    Type = t.Type
                })
                .ToListAsync();
        }

        // ✅ عرض التاريخ الكامل
        public async Task<List<TransactionResponse>> GetHistoryAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.Date)
                .Select(t => new TransactionResponse
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    Category = t.Category,
                    Type = t.Type
                })
                .ToListAsync();
        }

        // ✅ عرض الفئات مع إجمالي المصروفات
        public async Task<List<CategorySummaryResponse>> GetCategoriesSummaryAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId && t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category)
                .Select(g => new CategorySummaryResponse
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToListAsync();
        }

        // ✅ التقرير المالي الكامل
        public async Task<FinancialReportResponse> GetFinancialReportAsync(int userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId && t.Type == TransactionType.Expense)
                .ToListAsync();

            var total = transactions.Sum(t => t.Amount);
            var days = transactions.Select(t => t.Date.Date).Distinct().Count();
            var dailyAverage = days > 0 ? total / days : 0;

            var categories = transactions
                .GroupBy(t => t.Category)
                .Select(g => new CategorySummaryResponse
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToList();

            return new FinancialReportResponse
            {
                TotalExpenses = total,
                DailyAverage = dailyAverage,
                Categories = categories
            };
        }
    }
}
