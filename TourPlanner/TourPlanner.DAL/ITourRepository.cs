using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetToursAsync();
        Task<IEnumerable<TourLog>> GetTourLogsAsync();

        Task<int> InsertTourAsync(Tour tour);
        Task DeleteTourAsync(int tourID);
        Task UpdateTourAsync(Tour tour);

        Task<int> InsertTourLogAsync(int tourID, TourLog tourLog);
        Task DeleteTourLogAsync(int tourLogID);
        Task UpdateTourLogAsync(TourLog tourLog);
    }
}
