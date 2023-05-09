using TourPlanner.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TourPlanner.DAL
{
    public class APITourRepository : ITourRepository
    {
        const string
            ApiRouteTours = "api/tours",
            ApiRouteTourLogs = "api/tourlogs";

        HttpClient client;

        public bool Connect(Uri apiUrl)
        {
            client = new HttpClient();
            client.BaseAddress = apiUrl;
            return true;
        }
        
        public IEnumerable<Tour> GetTours()
        {
            var response = client.GetAsync(ApiRouteTours);
            response.Wait();
            var content = response.Result.Content.ReadAsStringAsync();
            content.Wait();
            return JsonConvert.DeserializeObject<Tour[]>(content.Result);
        }

        public IEnumerable<TourLog> GetTourLogs()
        {
            var response = client.GetAsync(ApiRouteTourLogs);
            response.Wait();
            var content = response.Result.Content.ReadAsStringAsync();
            content.Wait();
            return JsonConvert.DeserializeObject<TourLog[]>(content.Result);
        }

        public void DeleteTour(int tourID)
        {
            var response = client.DeleteAsync($"{ApiRouteTours}/{tourID}");
            response.Wait();
        }

        public void DeleteTourLog(int tourLogID)
        {
            var response = client.DeleteAsync($"{ApiRouteTourLogs}/{tourLogID}");
            response.Wait();
        }

        public void InsertTour(Tour tour)
        {
            var content = JsonContent.Create(tour);
            var response = client.PostAsync($"{ApiRouteTours}", content);
            response.Wait();
        }

        public void InsertTourLog(int tourID, TourLog tourLog)
        {
            var content = JsonContent.Create(tourLog);
            var response = client.PostAsync($"{ApiRouteTourLogs}", content);
            response.Wait();
        }

        public void UpdateTour(Tour tour)
        {
            var content = JsonContent.Create(tour);
            var response = client.PatchAsync($"{ApiRouteTours}", content);
            response.Wait();
        }

        public void UpdateTourLog(TourLog tourLog)
        {
            var content = JsonContent.Create(tourLog);
            var response = client.PatchAsync($"{ApiRouteTourLogs}", content);
            response.Wait();
        }
    }
}
