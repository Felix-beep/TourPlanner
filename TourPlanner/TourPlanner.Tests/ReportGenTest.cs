using TourPlanner.BL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class ReportGenTest
    {
        [Test]
        public void Test()
        {
            var gen = new ReportGenerator();

            var tourLogs = new TourLog[]
            {
                new TourLog {
                    ID = 1,
                    comment = "first TL",
                    date = DateTime.Now,
                    difficulty = 1,
                    totalTime = TimeSpan.FromHours(3),
                    rating = 4,
                },
                new TourLog {
                    ID = 2,
                    comment = "second TL",
                    date = DateTime.Now,
                    difficulty = 3,
                    totalTime = TimeSpan.FromHours(4),
                    rating = 5,
                },
            };

            var tour = new Tour
            {
                ID = 1,
                name = "ATour",
                description = "description of tour one",
                from = "placeA",
                to = "placeB",
                transportType = "skateboard",
                tourDistance = 1000,
                estimatedTime = TimeSpan.FromHours(2),
                imageID = "test.jpg",
                logs = tourLogs,
            };

            gen.GenerateTourPDF(tour, "output_report.pdf");
        }

    }
}
