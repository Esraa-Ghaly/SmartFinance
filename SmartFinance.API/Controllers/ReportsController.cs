using Microsoft.AspNetCore.Mvc;
using SmartFinance.Services; // ✅ علشان يشوف ReportService

namespace SmartFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GenerateReport(int userId)
        {
            var report = await _reportService.GenerateReportAsync(userId);
            return Ok(report);
        }
    }
}
