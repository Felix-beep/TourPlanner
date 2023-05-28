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
        public BackgroundLogic(ConnectionModeFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        // Logging
        readonly ILog log = LogManager.GetLogger(typeof(BackgroundLogic));


        // repository
        private ConnectionModeFactory _repositoryFactory;
        public bool SwapOnlineMode() => _repositoryFactory.SwapMode(); 

        // DB Access
        public async Task<IEnumerable<Tour>> GetAllToursAsync() 
            => await _repositoryFactory.GetRepo().GetToursAsync();

        public async Task<IEnumerable<TourLog>> GetTourLogsAsync(int TourID) 
            => await _repositoryFactory.GetRepo().GetTourLogsAsync();

        public async Task CreateNewTourAsync(Tour NewTour)
        {
            await _repositoryFactory.GetRepo().InsertTourAsync(NewTour);
        }

        public async Task EditTourAsync(Tour EditedTour)
        {
            await _repositoryFactory.GetRepo().UpdateTourAsync(EditedTour);
        }
        public async Task DeleteTourAsync(int TourID)
        {
            await _repositoryFactory.GetRepo().DeleteTourAsync(TourID);
        }

        // Export and Import
        public async Task ExportToursAsync(IEnumerable<Tour> toursToExport)
        {
            foreach (var tour in toursToExport) 
                await ExportTour(tour);
        }

        public async Task ImportToursAsync(IEnumerable<string> filesToImport)
        {
            foreach (var file in filesToImport)
                await ImportTour(file);
        }

        private async Task ImportTour(String fileToImport)
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

            var newTourID = await _repositoryFactory.GetRepo().InsertTourAsync(newTour);
            foreach (var log in tourLogs)
                await _repositoryFactory.GetRepo().InsertTourLogAsync(newTourID, log);

            log.Info($"imported tour {newTour.name}, with {tourLogs.Count} logs from file {fileToImport}");
        }

        private async Task ExportTour(Tour tour)
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


        // Full Text Search
        public async Task<IEnumerable<Tour>> FullTextSearchAsync(String Text)
        {
            FullTextSearchFactory factory = new();
            return factory.SearchForText((await _repositoryFactory.GetRepo().GetToursAsync()).ToList(), Text);
        }


    }
}
