using Microsoft.EntityFrameworkCore;
using SmartFinance.Infrastructure.Data;
using SmartFinance.Core.Services;
using SmartFinance.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure DbContext
builder.Services.AddDbContext<SmartFinanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ReportService>();

// ✅ Add Controllers
builder.Services.AddControllers();

// ✅ Swagger (لو محتاجة توثيق API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
