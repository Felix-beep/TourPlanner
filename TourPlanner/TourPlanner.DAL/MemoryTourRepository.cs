using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class MemoryTourRepository : ITourRepository
    {
        int nextTourID = 1;
        int nextTourLogID = 1;

        Dictionary<int, Tour> tours = new Dictionary<int, Tour>()
        {
            {0, new Tour(0, "Hello Subject1", "description1", new List<TourLog>())},
            {1,  new Tour(1, "Subject2", "description2", new List<TourLog>()) },
            {2, new Tour(2, "Subject3", "description3", new List<TourLog>()) },
            {3, new Tour(3, "Food", "Food", new List<TourLog>()) },
        };

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
