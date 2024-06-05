using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ScheduleService
{
    public class AuthenticateMe
    {
        public async static Task<(string apiKey, string token)> CallApiAndReturnCredentials(string apiKey)
        {
            HttpClient client = new();

            // Verify the correct header key name with the API documentation
            client.DefaultRequestHeaders.Add("api_key", apiKey);  // Adjust if needed
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "http://api.sahmab.co/accounts/terminal-access-token";

            // Prepare the request body if the API expects specific data
            string requestBody = $"{{\"api_key\": \"{apiKey}\"}}";

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize with the correct class structure
                    var credentials = JsonSerializer.Deserialize<CredentialsResponse>(responseContent);

                    // Access properties using exact names from the JSON response
                    return (credentials.data.api_key, credentials.data.access_token);
                }
                else
                {
                    // Log detailed error message and throw an exception
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API: {ex.Message}");
                return ("apikey", "token");
            }
        }

    }
    public class CredentialsResponse
    {
        public int error { get; set; }
        public Data? data { get; set; }
    }

    public class Data
    {
        public string? access_token { get; set; }
        public string? api_key { get; set; }
    }
}
