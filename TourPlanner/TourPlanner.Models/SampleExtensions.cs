namespace TourPlanner.Models
{
    public static class SampleExtensions
    {
        public static Tour CreateSampleTour(int i, ICollection<TourLog> logs)
            => new Tour
            {
                name = $"Tour{i}",
                description = $"Tour{i}Desc",
                from = $"from{i}",
                to = $"to{i}",
                transportType = "skateboard",
                tourDistance = 10 * i,
                estimatedTime = TimeSpan.FromHours(i),
                imageID = $"img{i}",
                logs = logs,
            };
    }
}
