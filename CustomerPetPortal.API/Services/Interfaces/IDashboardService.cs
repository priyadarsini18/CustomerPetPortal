using CustomerPetPortal.API.DTOs.Dashboard;

namespace CustomerPetPortal.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync(int customerId);
    }
}