using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IReportGenerator
    {
        void GenerateSummaryReport(IEnumerable<Tour> tours, string documentName);
        void GenerateTourReport(Tour tour, byte[] mapImageData, string documentName);
    }
}