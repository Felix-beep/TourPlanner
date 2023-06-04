namespace TourPlanner.Models
{
    public static class SampleExtensions
    {
        static readonly string[] locations = new string[]
        {
            "Vienna",
            "Linz",
            "Salzburg",
            "Graz",
            "Hamburg",
            "Berlin"
        };

        public static Tour CreateSampleTour(int i, ICollection<TourLog> logs)
        {
            return new Tour
            {
                name = $"Tour{i}",
                description = $"Tour{i}Desc",
                from = locations[i % locations.Length],
                to = locations[(i + 1) % locations.Length],
                transportType = "skateboard",
                tourDistance = 10 * i,
                estimatedTime = TimeSpan.FromHours(i),
                imageID = $"img{i}",
                logs = logs,
            };
        }
    }
}
