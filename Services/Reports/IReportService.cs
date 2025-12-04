using KundenUmfrageTool.Api.Dtos.Reports;

namespace KundenUmfrageTool.Api.Services.Reports
{
    public interface IReportService
    {
        Task<RestaurantReportDto> GetReportForRestaurant(int restaurantId);
    }
}
