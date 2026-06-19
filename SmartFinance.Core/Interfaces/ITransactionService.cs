using SmartFinance.Core.DTOs.Transactions;
using SmartFinance.Core.Models;

namespace SmartFinance.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(AddTransactionRequest request);
        Task<List<TransactionResponse>> GetTransactionsByUserAsync(int userId);
        Task<Transaction?> GetByIdAsync(int id);
        Task<Transaction?> UpdateTransactionAsync(int id, UpdateTransactionRequest request);
        Task<bool> DeleteTransactionAsync(int id);
        Task<List<TransactionResponse>> GetRecentTransactionsAsync(int userId);
        Task<List<TransactionResponse>> GetHistoryAsync(int userId);
        Task<List<CategorySummaryResponse>> GetCategoriesSummaryAsync(int userId);
        Task<FinancialReportResponse> GetFinancialReportAsync(int userId);
    }
}
