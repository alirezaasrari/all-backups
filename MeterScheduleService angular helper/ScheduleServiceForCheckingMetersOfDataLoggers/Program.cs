using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SchduleServiceForCheckingMetersOfDataloggers
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
                var ListOfData = await db.NewTerminal.OrderByDescending(a=>a.Id).Take(100).ToListAsync();
                var Check = await db.DataLoggerWaterMeter.ToListAsync();
                foreach (var item in ListOfData)
                {
                    var number = item.DataLoggerSerialInDecimal;
                    foreach(var index in Check)
                    {
                        if(index.DecimalNumber == number.ToString())
                        {
                            item.ListOfMeters = index.WaterMeterSerial;
                            item.NumberOfMeters = index.NumberOfMeters;
                            
                        }
                        db.SaveChanges();
                    }
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
