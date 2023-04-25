using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IBackgroundLogic
    {
        public IEnumerable<Tour> GetAllTours();

        public IEnumerable<TourLog> GetTourLogs(int TourID);

        public void CreateNewTour(String Name, String From, String To);

        public void EditDescription(int TourID, String Text);

        public void ExportTours(IEnumerable<Tour> ToursToExport);

        public void ImportTours(String FileToImport);

    }
}
