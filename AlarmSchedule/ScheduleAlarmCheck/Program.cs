using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleAlarmCheck;
using ScheduleService;
using System.Text.Json;

namespace ConsoleApp1
{
    public class ScheduleAlarmCheck
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

                    string apiUrl = "http://api.sahmab.co/manager/terminal/alarm-types?PerPage=1000";
                    apiKey = result.apiKey;
                    string token = result.token;
                    HttpResponseMessage response = await ApiCaller.CallGetApi(apiUrl, apiKey, token);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        ApiResponse? DeserializedResponse = JsonSerializer.Deserialize<ApiResponse>(content);

                        foreach (var alarm in DeserializedResponse.Data)
                        {
                            try
                            {
                                var newalarm = new Alarm
                                {
                                    Description = alarm.Title,
                                    Code = alarm.Code,
                                };
                                await db.Alarms.AddAsync(newalarm);
                                await db.SaveChangesAsync();
                                newalarm = new();
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex);
                            }
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
            Task.Run(async () =>
            {
                await InsertDataAsync();
            }).Wait();
        }
    }
}
