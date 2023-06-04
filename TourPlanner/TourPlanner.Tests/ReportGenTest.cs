using TourPlanner.BL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class ReportGenTest
    {
        Tour[] tours;

        [OneTimeSetUp] 
        public void SetUp() 
        {
            log4net.Config.BasicConfigurator.Configure();
            
            tours = new Tour[2];

            var tourLogs1 = new TourLog[]
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

            var tourLogs2 = new TourLog[]
            {
                new TourLog {
                    ID = 3,
                    comment = "third TL",
                    date = DateTime.Now,
                    difficulty = 2,
                    totalTime = TimeSpan.FromHours(3),
                    rating = 3,
                },
                new TourLog {
                    ID = 4,
                    comment = "fourth TL",
                    date = DateTime.Now,
                    difficulty = 4,
                    totalTime = TimeSpan.FromHours(1),
                    rating = 5,
                },
                new TourLog {
                    ID = 5,
                    comment = "fifth TL",
                    date = DateTime.Now,
                    difficulty = 5,
                    totalTime = TimeSpan.FromHours(4),
                    rating = 5,
                },
            };

            tours[0] = SampleExtensions.CreateSampleTour(0, tourLogs1);
            tours[1] = SampleExtensions.CreateSampleTour(1, tourLogs2);
            tours[0].ID = 0;
            tours[0].imageID = "test.jpg";
            tours[1].ID = 1;
        }

        [Test]
        public void TestSummaryReport()
        {
            var gen = new ITextReportGenerator();

            gen.GenerateSummaryReport(tours, "output_summary_report");

            Assert.IsTrue(File.Exists("output_summary_report.pdf"));
        }

        [Test]
        public void TestTourReport()
        {
            var gen = new ITextReportGenerator();

            gen.GenerateTourReport(tours[0], File.ReadAllBytes("test.jpg"), "output_tour_report");

            Assert.IsTrue(File.Exists("output_tour_report.pdf"));
        }
    }
}
