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

        public IEnumerable<Tour> GetAllTours()
        {
            return new List<Tour>(){
                new Tour{
                    ID=1,
                    name="Hello",
                    description="some description"},
                new Tour{
                    ID=2,
                    name="Hallo",
                    description="some description"},
                new Tour{
                    ID=3,
                    name="Tschau",
                    description="some description"},
            };
            //return _repository.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(int TourID)
        {
            return new List<TourLog>();
            //return _repository.GetTourLogs().Where(p => p.ID == TourID); // a function to get a specific TourLog from repository would be nice
        }

        public void CreateNewTour(String Name, String From, String To)
        {
            
        }

        public void EditDescription(int TourID, String Text)
        {

        }

        public void ExportTours(IEnumerable<Tour> ToursToExport)
        {

        }

        public void ImportTours(String FileToImport)
        {

        }

        public IEnumerable<Tour> FullTextSearch(String Text)
        {
            FullTextSearchFactory factory = new();
            return new List<Tour>();
        }

    }
}
