using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IBackgroundLogic
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<IEnumerable<TourLog>> GetTourLogsAsync(int TourID);
        Task CreateNewTourAsync(String Name, String Description, String From, String To);
        Task EditDescriptionAsync(int TourID, String Text);

        Task ExportToursAsync(IEnumerable<Tour> ToursToExport);
        Task ImportToursAsync(IEnumerable<String> FilesToImport);
        
        Task<IEnumerable<Tour>> FullTextSearchAsync(String Text);
    }
}
