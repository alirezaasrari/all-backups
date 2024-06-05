using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleService;
using System.Text.Json;

namespace ConsoleApp1
{
    public class ScheduleService
    {
        public async static Task InsertDataAsync()
        {
            var services = new ServiceCollection();
            var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";
            services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetService<DataContext>();
            try
            {
                string apiKey = "wIJ6!JWrM7bH*&Kpm@$CvACoR4Odg&%968fHz";
                {
                    var result = await AuthenticateMe.CallApiAndReturnCredentials(apiKey);

                    string apiUrl = "http://api.sahmab.co/manager/terminal/data-loggers";
                    apiKey = result.apiKey;
                    string token = result.token;
                    HttpResponseMessage response = await ApiCaller.CallGetApi(apiUrl, apiKey, token);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        ApiResponse? DeserializedResponse = JsonSerializer.Deserialize<ApiResponse>(content);
                        foreach (var dataLogger in DeserializedResponse.Data.Rows)
                        {
                                var newLogger = new DataLoggers
                                {
                                    DataLogger = dataLogger.Serial,
                                    Pk = (int)dataLogger.Fk_DataLoggerTypeId,
                                    CreatedAt = dataLogger.CreatedAt,
                                    DeletedAt = dataLogger.DeletedAt
                                };
                                await db.DataLogger.AddAsync(newLogger);
                                await db.SaveChangesAsync();
                            }
                    }
                    else
                    {
                        Console.WriteLine("not responding");
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Main()
        {
            Task.Run(async() => 
            {
                await InsertDataAsync();
            }).Wait();
        }
    }
}
