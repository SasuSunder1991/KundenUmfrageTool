using KundenUmfrageTool.Api.Dtos.Reports;

namespace KundenUmfrageTool.Api.Services
{
    public interface IReportService
    {
        Task<RestaurantReportDto> GetReportForRestaurant(int restaurantId);
    }
}
