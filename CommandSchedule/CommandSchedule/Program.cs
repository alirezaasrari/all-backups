using CommandSchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CommandSchedule
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

                    //string apiUrl = "http://api.sahmab.co/manager/terminal/commands?Page=1&PerPage=100&Filter_DataLogger_Serial=4014020003";
                    string apiUrl = "http://api.sahmab.co/manager/terminal/commands?Page=1&PerPage=100";
                    apiKey = result.apiKey;
                    string token = result.token;
                    HttpResponseMessage response = await ApiCaller.CallGetApi(apiUrl, apiKey, token);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        ApiResponse? DeserializedResponse = JsonSerializer.Deserialize<ApiResponse>(content);

                        foreach (var command in DeserializedResponse.Data)
                        {
                            var commandqueue = new CommandQueue
                            {
                                DcuSerial = command.Fk_DataLoggerSerial,
                                Code = command.Fk_CommandTypeCode,
                                setting_type = command.MetaData?.setting_type,
                                setting_value = command.MetaData?.setting_value,
                                Date = command.MetaData?.date,
                                MeterSerial = command.Fk_WaterMeterSerial,
                                Done = 0,
                            };
                            db.CommandQueue.Add(commandqueue);
                            await db.SaveChangesAsync();
                            commandqueue = new();
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
