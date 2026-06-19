namespace SmartFinance.Core.DTOs.Auth
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        // ✅ ندي قيمة افتراضية علشان نتفادى Error الـ Non-nullable
        public string FullName { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        // ✅ ندي قيمة افتراضية للـ Token
        public string Token { get; set; } = string.Empty; // JWT لاحقًا
    }
}
