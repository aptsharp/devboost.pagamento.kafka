using System.Net.Http;
using System.Net.Http.Headers;

namespace DroneDelivery.Helper.HttpUtils
{
    public static class HttpPostService
    {
        public static HttpResponseMessage PostService(string url, HttpContent httpContent, AuthenticationHeaderValue authenticationHeaderValue = null)
        {
            using var httpClient = new HttpClient();
            if (authenticationHeaderValue != null)
                httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            var result = httpClient.PostAsync(url, httpContent).Result;
            return result;
        }
    }
}
