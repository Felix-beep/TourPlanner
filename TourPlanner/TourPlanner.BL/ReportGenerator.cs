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
    public class ReportGenerator
    {
        public void GenerateSummaryPDF()
        {

        }

        public void GenerateTourPDF(Tour tour, string path)
        {
            var doc = new Document(
                new PdfDocument(new PdfWriter(path)));
        
            doc.Add(new Paragraph(tour.name + $" ({tour.ID})")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(14)
                    .SetBold()
                    .SetFontColor(ColorConstants.RED));
            doc.Add(new Paragraph(tour.description));

            doc.Add(new Paragraph("Properties")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(14)
                .SetBold()
                .SetFontColor(ColorConstants.BLUE));

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

            doc.Add(new Paragraph("Tour Log Table")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(18)
                .SetBold()
                .SetFontColor(ColorConstants.GREEN));
            var table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();

            table.AddHeaderCell(getHeaderCell(nameof(TourLog.ID)));
            table.AddHeaderCell(getHeaderCell(nameof(TourLog.comment)));
            table.AddHeaderCell(getHeaderCell(nameof(TourLog.date)));
            table.AddHeaderCell(getHeaderCell(nameof(TourLog.difficulty)));
            table.AddHeaderCell(getHeaderCell(nameof(TourLog.totalTime)));
            table.AddHeaderCell(getHeaderCell(nameof(TourLog.rating)));
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            foreach (var tourLog in tour.logs)
            {
                table.AddCell(tourLog.ID.ToString());
                table.AddCell(tourLog.comment);
                table.AddCell(tourLog.date.ToString());
                table.AddCell(tourLog.difficulty.ToString());
                table.AddCell(tourLog.totalTime.ToString());
                table.AddCell(tourLog.rating.ToString());
            }
            doc.Add(table);

            doc.Add(new Paragraph("Tour Image")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetFontSize(18)
                .SetBold()
                .SetFontColor(ColorConstants.GREEN));
            doc.Add(new Image(ImageDataFactory.Create(tour.imageID))
                .SetMaxWidth(500)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER));

            doc.Close();

            using var fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"\"{path}\"";
            fileOpener.Start();
        }

        private static Cell getHeaderCell(string s)
            => new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
    }
}
