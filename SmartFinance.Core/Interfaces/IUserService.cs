using SmartFinance.Core.DTOs.Auth;
using SmartFinance.Core.Models;

namespace SmartFinance.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequest request);
        Task<User?> LoginAsync(LoginRequest request);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();

        // ✅ دوال جديدة علشان تربط مع Google Auth
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);

        // ✅ دالة تحديث المستخدم (مهمة لإعادة تعيين كلمة المرور)
        Task<User> UpdateAsync(User user);
    }
}
