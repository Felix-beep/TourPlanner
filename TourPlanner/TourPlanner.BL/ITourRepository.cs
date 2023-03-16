using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface ITourRepository
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs();

        void InsertTour(Tour tour);
        void DeleteTour(int tourID);
        void UpdateTour(Tour tour);

        void InsertTourLog(Tour tourLog);
        void DeleteTourLog(int tourLogID);
        void UpdateTourLog(Tour tourLog);
    }
}
