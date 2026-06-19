using Microsoft.EntityFrameworkCore;
using SmartFinance.Services.Services;
using SmartFinance.Core.Models;
using SmartFinance.Core.DTOs.Auth; // هنا غيرنا الـ using علشان يشوف RegisterRequest و LoginRequest
using BCrypt.Net;
using SmartFinance.Infrastructure.Data;

namespace SmartFinance.Core.Services
{
    public class AuthService
    {
        private readonly SmartFinanceDbContext _context;

        public AuthService(SmartFinanceDbContext context)
        {
            _context = context;
        }

        // تسجيل مستخدم جديد
        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // تسجيل الدخول
        public async Task<User?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
