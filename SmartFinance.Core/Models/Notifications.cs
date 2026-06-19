namespace SmartFinance.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; // Info, Warning, Suggestion
        public string Message { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
