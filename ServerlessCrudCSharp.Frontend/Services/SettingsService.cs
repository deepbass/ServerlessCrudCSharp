using ServerlessCrudCSharp.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServerlessCrudCSharp.Frontend.Services
{
    public class SettingsService
    {
        private string SettingsRoute { get; set; }
        private Settings SettingsCache { get; set; }
        private HttpClient HttpClient { get; set; }
        public SettingsService(HttpClient httpClient)
        {
            HttpClient = httpClient;
            SettingsCache = null;
#if DEBUG
            SettingsRoute = "data/settings.local.json";
#else
            SettingsRoute = "data/settings.json";
#endif
        }

        public async Task<Settings> GetSettings()
        {
            if(SettingsCache == null)
            {
                SettingsCache = await HttpClient.GetFromJsonAsync<Settings>(SettingsRoute);
            }
            return SettingsCache;
        }
    }
}
