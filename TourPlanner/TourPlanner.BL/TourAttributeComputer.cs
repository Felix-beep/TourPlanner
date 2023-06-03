using TourPlanner.Models;

namespace TourPlanner.BL
{
    public static class TourAttributeComputer
    {
        public static double GetPopularity(this Tour tour)
        {
            return tour.logs.Count();
        }

        public static double GetChildFriendliness(this Tour tour) 
        {
            // lower value -> more child friendly

            return 
                tour.GetAverageDifficulty() * 
                tour.tourDistance * 
                tour.GetAverageTime();
        }

        public static double GetAverageTime(this Tour tour) 
        {
            return tour.logs.Average(tl => tl.totalTime.TotalSeconds);
        }

        public static double GetAverageDifficulty(this Tour tour)
        {
            return tour.logs.Average(tl => tl.difficulty);
        }

        public static double GetAverageRating(this Tour tour)
        {
            return tour.logs.Average(tl => tl.rating);
        }
    }
}
