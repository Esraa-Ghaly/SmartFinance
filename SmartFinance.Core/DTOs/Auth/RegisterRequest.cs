namespace SmartFinance.Core.DTOs.Auth
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;   // ✅ قيمة افتراضية
        public string Email { get; set; } = string.Empty;      // ✅ قيمة افتراضية
        public string Password { get; set; } = string.Empty;   // ✅ قيمة افتراضية
        public string ConfirmPassword { get; set; } = string.Empty; // ✅ قيمة افتراضية
    }
}
