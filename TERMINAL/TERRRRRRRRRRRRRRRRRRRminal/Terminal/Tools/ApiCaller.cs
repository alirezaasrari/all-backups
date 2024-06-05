using System.Net.Http.Headers;
using System.Text;
namespace Terminal.Tools
{
    public static class ApiCaller
    {
        public static async Task<HttpResponseMessage> CallPostApi(string apiUrl, string requestBody, string apiKey, string token)
        {
            using HttpClient client = new();
            // Add authorization header for token
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // Add custom header for apiKey (adjust name if needed)
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("apiKey", apiKey);
            }
            // Create StringContent (adjust as needed based on your API)
            StringContent content = new(requestBody, Encoding.UTF8, "application/json");
            // Send the POST request
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);     
            return response;
        }
    }
}
 