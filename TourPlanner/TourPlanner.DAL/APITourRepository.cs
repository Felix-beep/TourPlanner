using TourPlanner.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using log4net;

namespace TourPlanner.DAL
{
    public class APITourRepository : ITourRepository
    {
        readonly ILog log = LogManager.GetLogger(typeof(APITourRepository));

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
        
        public async Task<IEnumerable<Tour>> GetToursAsync()
        {
            var response = await client.GetAsync(ApiRouteTours);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tour[]>(content);
        }

        public async Task<IEnumerable<TourLog>> GetTourLogsAsync()
        {
            var response = await client.GetAsync(ApiRouteTourLogs);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TourLog[]>(content);
        }

        public async Task<Tour> GetTourAsync(int tourID)
        {
            var response = await client.GetAsync($"{ApiRouteTours}/{tourID}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tour>(content);
        }

        public async Task DeleteTourAsync(int tourID)
        {
            var response = await client.DeleteAsync($"{ApiRouteTours}/{tourID}");
        }

        public async Task DeleteTourLogAsync(int tourLogID)
        {
            var response = await client.DeleteAsync($"{ApiRouteTourLogs}/{tourLogID}");
        }

        public async Task<int> InsertTourAsync(Tour tour)
        {
            var content = JsonContent.Create(tour);
            var response = await client.PostAsync($"{ApiRouteTours}", content);
            var responseString = await response.Content.ReadAsStringAsync();
            log.Info($"got [{responseString}] after inserting tour from api");

            if (!int.TryParse(responseString, out var newTourID)) return -1;
            return newTourID;
        }

        public async Task<int> InsertTourLogAsync(int tourID, TourLog tourLog)
        {
            var content = JsonContent.Create(tourLog);
            var response = await client.PostAsync($"{ApiRouteTourLogs}/{tourID}", content);
            return int.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateTourAsync(Tour tour)
        {
            var content = JsonContent.Create(tour);
            log.Info($"requesting to update tour {tour.CustomToString()} as json:\n{await content.ReadAsStringAsync()}");
            var response = await client.PatchAsync($"{ApiRouteTours}", content);
        }

        public async Task UpdateTourLogAsync(TourLog tourLog)
        {
            var content = JsonContent.Create(tourLog);
            var response = await client.PatchAsync($"{ApiRouteTourLogs}", content);
        }
    }
}
