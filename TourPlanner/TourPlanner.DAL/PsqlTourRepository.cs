using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class PsqlTourRepository : ITourRepository
    {
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
                            comment = $"TourLog{j}"
                        });
             
                context.Tours.Add(
                    new Tour
                    {
                        name = $"Tour{i}",
                        description = $"Tour{i}Desc",
                        logs = logs
                    });
            }

            context.SaveChanges();
        }

        public IEnumerable<Tour> GetTours() => context.Tours.Where(t => true);
        public IEnumerable<TourLog> GetTourLogs() => context.TourLogs.Where(tl => true);

        public void DeleteTour(int tourID)
        {
            context.Tours.Remove(
                context.Tours.Single(t => t.ID == tourID));
            context.SaveChanges();
        }

        public void DeleteTourLog(int tourLogID)
        {
            context.TourLogs.Remove(
                context.TourLogs.Single(t => t.ID == tourLogID));
            context.SaveChanges();
        }

        public void InsertTour(Tour tour)
        {
            context.Tours.Add(tour);
            context.SaveChanges();
        }

        public void InsertTour(Tour tour, out int newTourID)
        {
            throw new NotImplementedException();
        }

        public void InsertTourLog(int tourID, TourLog tourLog)
        {
            context.TourLogs.Add(tourLog);
            context.SaveChanges();
        }

        public void UpdateTour(Tour tour)
        {
            context.Entry(context.Tours.Single(t => t.ID == tour.ID))
                .CurrentValues.SetValues(tour);
            context.SaveChanges();
        }

        public void UpdateTourLog(TourLog tourLog)
        {
            context.Entry(context.TourLogs.Single(tl => tl.ID == tourLog.ID))
                .CurrentValues.SetValues(tourLog);
            context.SaveChanges();
        }
    }
}
