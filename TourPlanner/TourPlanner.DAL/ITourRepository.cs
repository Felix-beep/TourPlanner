using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public interface ITourRepository
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs();

        void InsertTour(Tour tour);
        void DeleteTour(int tourID);
        void UpdateTour(Tour tour);

        void InsertTourLog(TourLog tourLog);
        void DeleteTourLog(int tourLogID);
        void UpdateTourLog(TourLog tourLog);
    }
}
