using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetPortal.API.Controllers
{
    [ApiController]
    [Route("api/excel")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExcelController(
            IExcelService excelService)
        {
            _excelService = excelService;
        }

        /// <summary>
        /// Import customer data from Excel file
        /// </summary>
        [HttpPost("customers/import")]
        public async Task<IActionResult> ImportCustomers(
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(
                    "Please upload a valid Excel file.");
            }

            await _excelService
                .ImportCustomersAsync(file);

            return Ok(new
            {
                Message = "Customer data imported successfully."
            });
        }

        /// <summary>
        /// Export customer data to Excel file
        /// </summary>
        [HttpGet("customers/export")]
        public async Task<IActionResult> ExportCustomers()
        {
            var excelFile =
                await _excelService
                    .ExportCustomersAsync();

            return File(
                excelFile,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Customers.xlsx");
        }
    }
}