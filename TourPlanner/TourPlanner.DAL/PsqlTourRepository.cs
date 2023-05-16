using log4net;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class PsqlTourRepository : ITourRepository
    {
        readonly ILog log = LogManager.GetLogger(typeof(PsqlTourRepository));

        PsqlContext context;

        public PsqlTourRepository(PsqlContext context)
        {
            this.context = context;

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public void PopulateWithSampleData()
        {
            for (int i = 0; i < 8; i++)
            {
                var logs = new List<TourLog>(); 
                for (int j = 0; j < i % 3; j++)
                    logs.Add(
                        new TourLog
                        {
                            comment = $"TourLog{j}",
                            date = DateTime.Now,
                            difficulty = i,
                            totalTime = TimeSpan.FromHours(i),
                            rating = i
                        });

                context.Tours.Add(SampleExtensions.CreateSampleTour(i, logs));
            }

            context.SaveChanges();
        }

        public async Task<IEnumerable<Tour>> GetToursAsync() => context.Tours.Where(t => true);
        public async Task<IEnumerable<TourLog>> GetTourLogsAsync() => context.TourLogs.Where(tl => true);
        public async Task<Tour> GetTourAsync(int tourID) => await context.Tours.FindAsync(tourID);

        public async Task DeleteTourAsync(int tourID)
        {
            context.Tours.Remove(
                await context.Tours.FindAsync(tourID));
            await context.SaveChangesAsync();
        }

        public async Task DeleteTourLogAsync(int tourLogID)
        {
            context.TourLogs.Remove(
                context.TourLogs.Single(t => t.ID == tourLogID));
            await context.SaveChangesAsync();
        }

        public async Task<int> InsertTourAsync(Tour tour)
        {
            context.Tours.Add(tour);
            await context.SaveChangesAsync();
            return tour.ID;
        }

        public async Task<int> InsertTourLogAsync(int tourID, TourLog tourLog)
        {
            context.TourLogs.Add(tourLog);
            await context.SaveChangesAsync();
            return tourLog.ID;
        }

        public async Task UpdateTourAsync(Tour tour)
        {
            var existingTour = await context.Tours.FindAsync(tour.ID);
            context.Entry(existingTour).CurrentValues.SetValues(tour);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTourLogAsync(TourLog tourLog)
        {
            context.Entry(context.TourLogs.Single(tl => tl.ID == tourLog.ID))
                .CurrentValues.SetValues(tourLog);
            await context.SaveChangesAsync();
        }
    }
}
