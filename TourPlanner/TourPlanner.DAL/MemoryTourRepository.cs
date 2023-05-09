using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class MemoryTourRepository : ITourRepository
    {
        int nextTourID = 1;
        int nextTourLogID = 1;

        Dictionary<int, Tour> tours = new Dictionary<int, Tour>();

        public void DeleteTour(int tourID) => tours.Remove(tourID);

        public void DeleteTourLog(int tourLogID)
        {
            foreach (var tour in tours.Values)
            {
                var tourLog = tour.logs.FirstOrDefault(tl => tl.ID == tourLogID);
                if (tourLog != default)
                {
                    tour.logs.Remove(tourLog);
                    return;
                }
            }
        }

        public IEnumerable<TourLog> GetTourLogs() => tours.SelectMany(t => t.Value.logs);

        public IEnumerable<Tour> GetTours() => tours.Values;

        public void InsertTour(Tour tour)
        {
            var newID = nextTourID++;
            tour.ID = newID;
            tours[newID] = tour;
        }

        public void InsertTourLog(int tourID, TourLog tourLog)
        {
            var newID = nextTourLogID++;
            tourLog.ID = newID;
            tours[tourID].logs.Add(tourLog);
        }

        public void UpdateTour(Tour tour)
            => tours[tour.ID] = tour;
        
        public void UpdateTourLog(TourLog tourLog)
        {
            foreach (var tour in tours.Values)
            {
                var existingTourLog = tour.logs.FirstOrDefault(tl => tl.ID == tourLog.ID);
                if (existingTourLog != default)
                {
                    existingTourLog.CopyFrom(tourLog);
                    return;
                }
            }
        }
    }
}
