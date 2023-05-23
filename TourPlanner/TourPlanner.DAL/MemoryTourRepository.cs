using log4net;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class MemoryTourRepository : ITourRepository
    {
        readonly ILog log = LogManager.GetLogger(typeof(MemoryTourRepository));

        int nextTourID = 1;
        int nextTourLogID = 1;

        Dictionary<int, Tour> tours = new Dictionary<int, Tour>();

        public async Task LoadSampleDataAsync()
        {
            var samples = new Tour[]
            {
                new Tour(1, "Hello Subject1", "description1", new List<TourLog>()),
                new Tour(2, "Subject2", "description2", new List<TourLog>()),
                new Tour(3, "Subject3", "description3", new List<TourLog>()),
                new Tour(4, "Food", "Food", new List<TourLog>()),
            };

            foreach (var sample in samples) await InsertTourAsync(sample);

            await InsertTourLogAsync(2,
                new TourLog
                {
                    comment = "a tour log",
                    date = DateTime.Now,
                    difficulty = 2,
                    totalTime = TimeSpan.FromHours(3),
                    rating = 5
                });
        }

        public async Task DeleteTourAsync(int tourID) => tours.Remove(tourID);

        public async Task DeleteTourLogAsync(int tourLogID)
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

        public async Task<IEnumerable<TourLog>> GetTourLogsAsync() => tours.SelectMany(t => t.Value.logs);

        public async Task<IEnumerable<Tour>> GetToursAsync() => tours.Values;

        public async Task<Tour> GetTourAsync(int tourID) => tours[tourID];
        public async Task<TourLog> GetTourLogAsync(int tourLogID) => tours.SelectMany(t => t.Value.logs).Single(tl => tl.ID == tourLogID);

        public async Task<int> InsertTourAsync(Tour tour)
        {
            var newID = nextTourID++;
            log.Info($"inserting tour {tour.name} with id {newID} into {nameof(MemoryTourRepository)}");
            tour.ID = newID;
            tours[newID] = tour;
            return newID;
        }

        public async Task<int> InsertTourLogAsync(int tourID, TourLog tourLog)
        {
            var newID = nextTourLogID++;
            log.Info($"inserting tour log [{tourLog.comment}] with id {newID} into {nameof(MemoryTourRepository)}");
            tourLog.ID = newID;
            tours[tourID].logs.Add(tourLog);
            return newID;
        }

        public async Task UpdateTourAsync(Tour tour)
            => tours[tour.ID] = tour;
        
        public async Task UpdateTourLogAsync(TourLog tourLog)
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
