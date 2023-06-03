using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Diagnostics;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    public class ITextReportGenerator : IReportGenerator
    {
        static Paragraph CreateTitle(string text, Color color)
        {
            return new Paragraph(text)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(14)
                .SetBold()
                .SetFontColor(color);
        }

        static Cell GetHeaderCell(string s)
            => new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        
        static Table CreateTourLogTable(Tour tour)
        {
            var table = new Table(UnitValue.CreatePercentArray(6))
                .UseAllAvailableWidth()
                .SetFontSize(14)
                .SetBackgroundColor(ColorConstants.WHITE)
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.ID)))
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.comment)))
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.date)))
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.difficulty)))
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.totalTime)))
                .AddHeaderCell(GetHeaderCell(nameof(TourLog.rating)));

            foreach (var tourLog in tour.logs)
            {
                table
                    .AddCell(tourLog.ID.ToString())
                    .AddCell(tourLog.comment)
                    .AddCell(tourLog.date.ToString())
                    .AddCell(tourLog.difficulty.ToString())
                    .AddCell(tourLog.totalTime.ToString())
                    .AddCell(tourLog.rating.ToString());
            }

            return table;
        }

        static Table CreateTourTable(IEnumerable<Tour> tours)
        {
            var table = new Table(UnitValue.CreatePercentArray(6))
                .UseAllAvailableWidth()
                .SetFontSize(14)
                .SetBackgroundColor(ColorConstants.WHITE)
                .AddHeaderCell(GetHeaderCell("Tour ID"))
                .AddHeaderCell(GetHeaderCell("Name"))
                .AddHeaderCell(GetHeaderCell("Avg. Time"))
                .AddHeaderCell(GetHeaderCell("Avg. Difficulty"))
                .AddHeaderCell(GetHeaderCell("Avg. Rating"))
                .AddHeaderCell(GetHeaderCell("Child Friendliness*"));

            foreach (var tour in tours) 
            {
                table
                    .AddCell(tour.ID.ToString())
                    .AddCell(tour.name)
                    .AddCell(TimeSpan.FromSeconds(tour.GetAverageTime()).ToString())
                    .AddCell(tour.GetAverageDifficulty().ToString())
                    .AddCell(tour.GetAverageRating().ToString())
                    .AddCell(tour.GetChildFriendliness().ToString());
            }

            return table;
        }

        public void GenerateSummaryReport(IEnumerable<Tour> tours, string documentName)
        {
            var filePath = $"{documentName}.pdf";

            var doc = new Document(
                new PdfDocument(new PdfWriter(filePath)));

            doc.Add(CreateTitle("Summary Report", ColorConstants.RED));
            doc.Add(new Paragraph($"This report contains the compiled data of {tours.Count()} tours for statistical analysis."));

            doc.Add(CreateTitle("Tour Table", ColorConstants.GREEN));
            doc.Add(CreateTourTable(tours));

            doc.Add(CreateTitle("Notes", ColorConstants.BLUE));
            doc.Add(new Paragraph("*Child friendliness formula (lower value is more child friendly): avg. difficulty * distance * avg. time"));

            doc.Close();

            using var fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"\"{filePath}\"";
            fileOpener.Start();
        }

        public void GenerateTourReport(Tour tour, byte[] mapImageData, string documentName)
        {
            var filePath = $"{documentName}.pdf";

            var doc = new Document(
                new PdfDocument(new PdfWriter(filePath)));

            doc.Add(CreateTitle($"{tour.name} (ID: {tour.ID})", ColorConstants.RED));
            doc.Add(new Paragraph(tour.description));

            doc.Add(CreateTitle("Properties", ColorConstants.BLUE));

            var list = new List()
                    .SetSymbolIndent(12)
                    .SetListSymbol("\u2022")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            list
                .Add(new ListItem($"from: {tour.from}"))
                .Add(new ListItem($"to: {tour.to}"))
                .Add(new ListItem($"transport type: {tour.transportType}"))
                .Add(new ListItem($"distance: {tour.tourDistance}m"))
                .Add(new ListItem($"est. time: {tour.estimatedTime}"));
            doc.Add(list);

            doc.Add(CreateTitle("Tour Log Table", ColorConstants.GREEN));
            doc.Add(CreateTourLogTable(tour));

            doc.Add(CreateTitle("Tour Image", ColorConstants.GREEN));
            doc.Add(new Image(ImageDataFactory.Create(mapImageData))
                .SetMaxWidth(500)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER));

            doc.Close();

            using var fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"\"{filePath}\"";
            fileOpener.Start();
        }
    }
}
