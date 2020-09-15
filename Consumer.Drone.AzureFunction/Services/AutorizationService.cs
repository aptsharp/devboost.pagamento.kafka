using Consumer.Drone.AzureFunction.Dto;
using Consumer.Drone.AzureFunction.Services.Interfaces;
using DroneDelivery.Helper.HttpUtils;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Drone.AzureFunction.Services
{
    public class AutorizationService : IAutorizationService
    {
        readonly string _url;

        public AutorizationService(string url)
        {
            _url = (string.IsNullOrEmpty(url))
                ? "https://localhost:44391/api/Authentication/Login"
                : url;
        }

        public async Task<string> GetToken(AuthenticationRequest authenticationRequest)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(authenticationRequest), Encoding.UTF8, "application/json");
            var result = HttpPostService.PostService(_url, jsonContent, null);
            if (result.StatusCode != HttpStatusCode.OK)
                return await Task.FromResult(string.Empty);
            else
            {
                var contentBody = result.Content.ReadAsStringAsync().Result;
                var contentResult = JsonConvert.DeserializeObject<LoginResult>(contentBody);
                return await Task.FromResult(contentResult.Token);
            }
        }
    }
}
