using ClosedXML.Excel;
using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Services.Interfaces;
using CustomerPetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Services
{
    public class ExcelService : IExcelService
    {
        private readonly AppDbContext _context;

        public ExcelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ImportCustomersAsync(
            IFormFile file)
        {
            using var stream =
                file.OpenReadStream();

            using var workbook =
                new XLWorkbook(stream);

            var worksheet =
                workbook.Worksheet("Customers");

            var rows =
                worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                string email =
                    row.Cell(3).GetValue<string>();

                bool exists =
                    await _context.Customers
                        .AnyAsync(
                            c => c.Email == email);

                if (exists)
                    continue;

                var customer = new Customer
                {
                    FirstName =
                        row.Cell(1).GetValue<string>(),

                    LastName =
                        row.Cell(2).GetValue<string>(),

                    Email = email,

                    Phone =
                        row.Cell(4).GetValue<string>(),

                    CommunicationPreference =
                        row.Cell(5).GetValue<string>(),

                    IsActive = true
                };

                _context.Customers.Add(customer);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> ExportCustomersAsync()
        {
            var customers =
                await _context.Customers
                    .ToListAsync();

            using var workbook =
                new XLWorkbook();

            var worksheet =
                workbook.Worksheets
                    .Add("Customers");

            worksheet.Cell(1, 1).Value =
                "FirstName";

            worksheet.Cell(1, 2).Value =
                "LastName";

            worksheet.Cell(1, 3).Value =
                "Email";

            worksheet.Cell(1, 4).Value =
                "Phone";

            worksheet.Cell(1, 5).Value =
                "CommunicationPreference";

            int row = 2;

            foreach (var customer in customers)
            {
                worksheet.Cell(row, 1).Value =
                    customer.FirstName;

                worksheet.Cell(row, 2).Value =
                    customer.LastName;

                worksheet.Cell(row, 3).Value =
                    customer.Email;

                worksheet.Cell(row, 4).Value =
                    customer.Phone;

                worksheet.Cell(row, 5).Value =
                    customer.CommunicationPreference;

                row++;
            }

            using var stream =
                new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}