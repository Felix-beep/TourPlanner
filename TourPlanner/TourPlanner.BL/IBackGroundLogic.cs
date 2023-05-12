using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IBackgroundLogic
    {
        public IEnumerable<Tour> GetAllTours();

        public IEnumerable<TourLog> GetTourLogs(int TourID);

        public void CreateNewTour(String Name, String Description, String From, String To);

        public void EditDescription(int TourID, String Text);

        public void ExportTours(IEnumerable<Tour> ToursToExport);

        public void ImportTours(IEnumerable<String> FilesToImport);

        public IEnumerable<Tour> FullTextSearch(String Text);

        public bool SwapOnlineMode();

    }
}
