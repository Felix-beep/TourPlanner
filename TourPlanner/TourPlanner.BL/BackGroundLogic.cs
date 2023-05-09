using System.Globalization;
using CsvHelper;
using TourPlanner.BL.FullTextSearch;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    public class BackgroundLogic : IBackgroundLogic
    {
        private ITourRepository _repository;
        public BackgroundLogic(ITourRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tour> GetAllTours() {
            return _repository.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(int TourID) => _repository.GetTourLogs();

        public void CreateNewTour(String Name, String From, String Description, String To)
        {
            Tour newTour = new()
            {
                name = Name,
                description = Description,
                // this still needs the information from MapQuest
            };

            _repository.InsertTour(newTour);
        }

        public void EditDescription(int TourID, String Text)
        {
            // not sure how to implement this yet
        }

        public void ExportTours(IEnumerable<Tour> ToursToExport)
        {
            // create csv File of the ToursToExport and return it
        }

        public void ImportTours(String FileToImport)
        {
            List<Tour> imports = new();

            try
            {
                using(var reader = new StreamReader(FileToImport))
                {
                    using (var csvreader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        List<Tour> records = csvreader.GetRecords<Tour>().ToList();
                    }
                }
            }
            catch
            {
                // Error
            }
        }

        public IEnumerable<Tour> FullTextSearch(String Text)
        {
            FullTextSearchFactory factory = new();
            return factory.SearchForText(_repository.GetTours().ToList(), Text);
        }

    }
}
