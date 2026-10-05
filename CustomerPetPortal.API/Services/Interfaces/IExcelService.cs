using Microsoft.AspNetCore.Http;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IExcelService
    {
        Task ImportCustomersAsync(IFormFile file);

        Task<byte[]> ExportCustomersAsync();
    }
}