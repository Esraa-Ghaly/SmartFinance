using Microsoft.EntityFrameworkCore;
using SmartFinance.Infrastructure.Data;
using SmartFinance.Core.Models;

namespace SmartFinance.Services.Services
{
    public class NotificationService
    {
        private readonly SmartFinanceDbContext _context;

        public NotificationService(SmartFinanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotificationsAsync(int userId)
        {
            return await _context.Notifications
                                 .Where(n => n.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<Notification> AddNotificationAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                Type = "Info"
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
