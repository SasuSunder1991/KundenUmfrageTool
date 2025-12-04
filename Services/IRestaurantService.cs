namespace KundenUmfrageTool.Api.Services
{
    public interface IRestaurantService
    {
        // Holt alle Restaurants (nur QM-Manager)
        Task<List<RestaurantDto>> GetAllAsync();

        // Holt nur Restaurants eines bestimmten Managers
        Task<List<RestaurantDto>> GetForManagerAsync(int managerUserId);


        // Holt ein einzelnes Restaurant per ID
        Task<RestaurantDto?> GetByIdAsync(int id);

        // Legt ein Restaurant an
        Task<RestaurantDto> CreateAsync(RestaurantDto dto);

        // Legt ein Restaurant an
        Task UpdateAsync(int id, RestaurantDto dto);
        // Löscht ein Restaurant
        Task DeleteAsync(int id);
    }
}


/*
   
Das Interface definiert, welche Funktionen der Restaurant-Service haben muss.
Der Controller spricht nur mit dem Interface, dadurch ist das Backend sauber strukturiert, testbar und flexibel.
 
 
 
 */