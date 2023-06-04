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

        Task CreateNewTourLogAsync(int TourId, TourLog NewTourLog);
        Task EditTourLogAsync(int TourId, TourLog TourLog);
        Task DeleteTourLogAsync(int TourId, int LogId);

        Task ExportToursAsync(IEnumerable<Tour> ToursToExport);
        Task ImportToursAsync(IEnumerable<String> FilesToImport);

        Task<byte[]> GetCachedImage(string imageID);
        Task CreateReport(Tour Tour);
        Task CreateSummaryReport(IEnumerable<Tour> Tours);

        Task<IEnumerable<Tour>> FullTextSearchAsync(String Text);

        bool SwapOnlineMode();

    }
}
