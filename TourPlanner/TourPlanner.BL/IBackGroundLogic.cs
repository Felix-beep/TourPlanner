using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IBackgroundLogic
    {
        public IEnumerable<Tour> GetAllTours();

        public IEnumerable<TourLog> GetTourLogs(int TourID);

        public void CreateNewTour(Tour NewTour);

        public void EditTour(Tour EditedTour);

        public void ExportTours(IEnumerable<Tour> ToursToExport);

        public void ImportTours(IEnumerable<String> FilesToImport);

        public IEnumerable<Tour> FullTextSearch(String Text);

        public bool SwapOnlineMode();

    }
}
