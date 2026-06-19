using SmartFinance.Core.DTOs.Auth;
using SmartFinance.Core.Interfaces;
using SmartFinance.Core.Models;
using SmartFinance.Core.Security;
using SmartFinance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartFinance.Services
{
    public class UserService : IUserService
    {
        private readonly SmartFinanceDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(SmartFinanceDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new ArgumentException("كلمة السر وتأكيدها غير متطابقين");

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new ArgumentException("هذا البريد الإلكتروني مسجل بالفعل");

            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;

            var isValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
            return isValid ? user : null;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Transactions)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // ✅ دوال جديدة علشان تربط مع Google Auth
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // ✅ دالة تحديث المستخدم (مهمة لإعادة تعيين كلمة المرور)
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
