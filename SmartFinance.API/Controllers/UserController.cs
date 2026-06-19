using Microsoft.AspNetCore.Mvc;
using SmartFinance.Core.Models;
using SmartFinance.Infrastructure.Data;

namespace SmartFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SmartFinanceDbContext _context;

        public UserController(SmartFinanceDbContext context)
        {
            _context = context;
        }

        // تحديث الملف الشخصي
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateProfile(int userId, User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.FullName = updatedUser.FullName; // ✅ استخدم FullName بدل Name
            user.Email = updatedUser.Email;
            user.PasswordHash = updatedUser.PasswordHash; // لو عندك خاصية الباسورد مشفر

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // تسجيل الخروج
        [HttpPost("logout/{userId}")]
        public IActionResult Logout(int userId)
        {
            return Ok(new { message = "تم تسجيل الخروج بنجاح" });
        }

        // حذف الحساب
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteAccount(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم حذف الحساب بنجاح" });
        }
    }
}
