using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IBackendLogic
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<IEnumerable<TourLog>> GetTourLogsAsync(int TourID);

        Task CreateNewTourAsync(Tour NewTour);

        Task EditTourAsync(Tour EditedTour);

        Task DeleteTourAsync(int TourID);

        Task ExportToursAsync(IEnumerable<Tour> ToursToExport);

        Task ImportToursAsync(IEnumerable<String> FilesToImport);

        Task<IEnumerable<Tour>> FullTextSearchAsync(String Text);

        bool SwapOnlineMode();
    }
}
