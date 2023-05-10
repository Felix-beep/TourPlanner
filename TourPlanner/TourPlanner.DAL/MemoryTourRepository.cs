using log4net;
using Npgsql.Internal;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class MemoryTourRepository : ITourRepository
    {
        readonly ILog log = LogManager.GetLogger(typeof(MemoryTourRepository));

        int nextTourID = 1;
        int nextTourLogID = 1;

        Dictionary<int, Tour> tours = new Dictionary<int, Tour>();

        public void LoadSampleData()
        {
            var samples = new Tour[]
            {
                new Tour(1, "Hello Subject1", "description1", new List<TourLog>()),
                new Tour(2, "Subject2", "description2", new List<TourLog>()),
                new Tour(3, "Subject3", "description3", new List<TourLog>()),
                new Tour(4, "Food", "Food", new List<TourLog>()),
            };

            foreach (var sample in samples)
                InsertTour(sample);

            InsertTourLog(2,
                new TourLog
                {
                    comment = "a tour log",
                    date = DateTime.Now,
                    difficulty = 2,
                    totalTime = TimeSpan.FromHours(3),
                    rating = 5
                });
        }

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

        public void InsertTour(Tour tour) => InsertTour(tour, out var _);

        public void InsertTour(Tour tour, out int newTourID)
        {
            var newID = nextTourID++;
            log.Info($"inserting tour {tour.name} with id {newID} into {nameof(MemoryTourRepository)}");
            tour.ID = newID;
            tours[newID] = tour;
            newTourID = newID;
        }

        public void InsertTourLog(int tourID, TourLog tourLog)
        {
            var newID = nextTourLogID++;
            log.Info($"inserting tour log [{tourLog.comment}] with id {newID} into {nameof(MemoryTourRepository)}");
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
