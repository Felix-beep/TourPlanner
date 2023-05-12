using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using log4net;
using TourPlanner.BL.FullTextSearch;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    public class BackgroundLogic : IBackgroundLogic
    {
        readonly ILog log = LogManager.GetLogger(typeof(BackgroundLogic));

        public ConnectionModeFactory _repositoryFactory;
        public bool SwapOnlineMode() => _repositoryFactory.SwapMode(); 

        public BackgroundLogic(ConnectionModeFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public IEnumerable<Tour> GetAllTours() {
            return _repositoryFactory.GetRepo().GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(int TourID) => _repositoryFactory.GetRepo().GetTourLogs();

        public void CreateNewTour(String Name, String From, String Description, String To)
        {
            Tour newTour = new()
            {
                name = Name,
                description = Description,
                // this still needs the information from MapQuest
            };

            _repositoryFactory.GetRepo().InsertTour(newTour);
        }

        public void EditDescription(int TourID, String Text)
        {
            // not sure how to implement this yet
        }

        public void ExportTours(IEnumerable<Tour> toursToExport)
        {
            foreach (var tour in toursToExport) 
                ExportTour(tour);
        }

        void ExportTour(Tour tour)
        {
            // the export format is as follows: (line by line)
            // tour header
            // tour data
            // tourlog header
            // tourlog data (REPEATING)

            // for example tour with 2 logs:

            // ID,name,description,from,to,transportType,tourDistance,estimatedTime,imageID
            // 2,Subject2,description2,,,,0,00:00:00,
            // ID,comment,date,difficulty,totalTime,rating
            // 1,a tour log,05 / 10 / 2023 16:44:54,2,03:00:00,5
            // 2,a second tour log,05 / 10 / 2023 16:44:54,2,03:00:00,5

            var fileName = $"{tour.name}.csv";
            log.Info($"writing tour {tour.name} to file [{fileName}]");

            using var writer = new StreamWriter(fileName);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteHeader<Tour>();
            csvWriter.NextRecord();

            csvWriter.WriteRecord(tour);
            csvWriter.NextRecord();

            csvWriter.WriteHeader<TourLog>();
            csvWriter.NextRecord();

            csvWriter.WriteRecords(tour.logs);
            csvWriter.NextRecord();
        }

        public void ImportTours(IEnumerable<string> filesToImport)
        {
            foreach (var file in filesToImport)
                ImportTour(file);
        }

        void ImportTour(String fileToImport)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);

            Tour newTour = null;
            List<TourLog> tourLogs = new();

            using var fileStream = new FileStream(fileToImport, FileMode.Open);
            using var reader = new StreamReader(fileStream);
            using var csvReader = new CsvReader(reader, csvConfig, true);

            csvReader.Read();
            csvReader.ReadHeader();

            csvReader.Read();
            newTour = csvReader.GetRecord<Tour>();

            csvReader.Read();
            csvReader.ReadHeader();

            while (csvReader.Read())
                tourLogs.Add(csvReader.GetRecord<TourLog>());

            newTour.logs = new List<TourLog>();

            _repositoryFactory.GetRepo().InsertTour(newTour, out var newTourID);
            foreach (var log in tourLogs)
                _repositoryFactory.GetRepo().InsertTourLog(newTourID, log);

            log.Info($"imported tour {newTour.name}, with {tourLogs.Count} logs from file {fileToImport}");
        }

        public IEnumerable<Tour> FullTextSearch(String Text)
        {
            FullTextSearchFactory factory = new();
            return factory.SearchForText(_repositoryFactory.GetRepo().GetTours().ToList(), Text);
        }

    }
}
