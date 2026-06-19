using Microsoft.EntityFrameworkCore;
using SmartFinance.Core.Models; // ✅ علشان يشوف الـ User, Transaction, Category, Notification

namespace SmartFinance.Infrastructure.Data
{
    public class SmartFinanceDbContext : DbContext
    {
        public SmartFinanceDbContext(DbContextOptions<SmartFinanceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // ✅ Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "طعام ومشروبات" },
                new Category { Id = 2, Name = "تسوق" },
                new Category { Id = 3, Name = "سفر" },
                new Category { Id = 4, Name = "صحة وعلاج" },
                new Category { Id = 5, Name = "مواصلات" },
                new Category { Id = 6, Name = "فواتير" },
                new Category { Id = 7, Name = "تعليم ودورات" },
                new Category { Id = 8, Name = "ترفيه" },
                new Category { Id = 9, Name = "ادخار" },
                new Category { Id = 10, Name = "صدقات وتبرعات" },
                new Category { Id = 11, Name = "صيانة وإصلاحات" },
                new Category { Id = 12, Name = "أقساط وديون" },
                new Category { Id = 13, Name = "أخرى" }
            );
        }
    }
}
