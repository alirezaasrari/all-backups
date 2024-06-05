using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace ScheduleServiceForWaterMetersOfDatalogger
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
                    List<string> ListOfDataLoggers = new();
                    var PkDataLoggerIds = await db.DataLogger.Select(x => x.Pk_DataLoggerId).ToListAsync();
                    
                    foreach(var index in PkDataLoggerIds)
                    {
                        ListOfDataLoggers.Add(index.ToString());                  
                    }
                    foreach(var dataloggerserial in ListOfDataLoggers)
                    {
                    string apiKey = "wIJ6!JWrM7bH*&Kpm@$CvACoR4Odg&%968fHz";
                    var result = await AuthenticateMe.CallApiAndReturnCredentials(apiKey);              
                    apiKey = result.apiKey;
                    string token = result.token;
                    string apiUrl = $"http://api.sahmab.co/manager/terminal/data-loggers/{dataloggerserial}";
                    HttpResponseMessage response = await ApiCaller.CallGetApi(apiUrl, apiKey, token);
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            ApiResponse? DeserializedResponse = JsonSerializer.Deserialize<ApiResponse>(content);
                            string DecimalDataLoggerNumber = DeserializedResponse.Data.Serial;
                            int NumberOfMeters = (int)(DeserializedResponse.Data.WaterMeters.Count());
                            if(DeserializedResponse.Data.WaterMeters.Count != 0)
                            {
                                List<string> List = new();
                                DataLoggerWaterMeter dataloggerwatermeter = new();
                                foreach (var watermeter in DeserializedResponse.Data.WaterMeters)
                                {
                                   List.Add(watermeter.Serial);                                  
                                }
                                dataloggerwatermeter.DataLoggerSerial = dataloggerserial;
                                dataloggerwatermeter.NumberOfMeters = NumberOfMeters;
                                dataloggerwatermeter.DecimalNumber = DecimalDataLoggerNumber;
                                dataloggerwatermeter.WaterMeterSerial = String.Join(",", List);
                                await db.DataLoggerWaterMeter.AddAsync(dataloggerwatermeter);
                                await db.SaveChangesAsync();
                                dataloggerwatermeter = new();
                            }                    
                        }
                        else
                        {
                            Console.WriteLine("not responding");
                        };
                    }
                    //var dataloggers = await db.DataLogger.ToListAsync();
                    //var watermeters = await db.DataLoggerWaterMeter.ToListAsync();
                    //foreach(var datalogger in dataloggers)
                    //{
                    //   foreach(var meter in watermeters)
                    //   {
                    //      if (datalogger.Pk_DataLoggerId = (int)meter.DataLoggerSerial)
                    //       {
                              
                    //       }
                    //   }
                      
                    //}
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
