using System.Net.Http.Headers;

namespace ScheduleService
{
    public class ApiCaller
    {
        public static async Task<HttpResponseMessage> CallGetApi(string apiUrl, string apiKey, string token)
        {
            try
            {
                using HttpClient client = new();
                // Add authorization header
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                // Add custom header for apiKey (adjust name if needed)
                if (!string.IsNullOrEmpty(apiKey))
                {
                    client.DefaultRequestHeaders.Add("apiKey", apiKey);
                }

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                return response;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                throw new Exception("Error calling API: " + ex.Message);
            }
        }
    }

}
