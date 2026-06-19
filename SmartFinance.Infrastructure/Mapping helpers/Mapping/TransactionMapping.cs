using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartFinance.Core.Models;

namespace SmartFinance.Infrastructure.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // اسم الجدول
            builder.ToTable("Transactions");

            // المفتاح الأساسي
            builder.HasKey(t => t.Id);

            // الخصائص
            builder.Property(t => t.Id)
                   .IsRequired();

            builder.Property(t => t.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            builder.Property(t => t.Date)
                   .IsRequired();

            builder.Property(t => t.Type)
                   .IsRequired()
                   .HasConversion<string>(); // يحفظ الـ Enum كـ string

            // العلاقة مع User
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Transactions)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
