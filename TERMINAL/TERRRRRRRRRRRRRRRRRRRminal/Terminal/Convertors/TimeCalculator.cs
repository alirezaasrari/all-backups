using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text;
using Terminal.Context;
using Terminal.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Terminal.Convertors
{
    public static class TimeCalculator
    {
       public static string ConvertMiladiToshamsi() 
        {
            
            try
            {
                var services = new ServiceCollection();

                // this is the connection string for communication with the database
                var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";

                // in this part we prepare for using entity framework core context as a connectin bridge with the table and models
                services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<DataContext>();
                DateTime now = DateTime.Now;
                PersianCalendar persianCalendar = new();
                int persianYear = persianCalendar.GetYear(now);
                int persianMonth = persianCalendar.GetMonth(now);
                int persianDay = persianCalendar.GetDayOfMonth(now);
                int persianHour = persianCalendar.GetHour(now);
                int persianMinute = persianCalendar.GetMinute(now);
                int persianSecond = persianCalendar.GetSecond(now);
                StringBuilder persianDateTimeString = new();
                persianDateTimeString.Append($"{persianYear:0000}-{persianMonth:00}-{persianDay:00} ");
                persianDateTimeString.Append($"{persianHour:00}:{persianMinute:00}:{persianSecond:00}");
                return (persianDateTimeString.ToString());
            }
            catch (Exception ex )
            {
                var services = new ServiceCollection();

                // this is the connection string for communication with the database
                var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";

                // in this part we prepare for using entity framework core context as a connectin bridge with the table and models
                services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<DataContext>();
                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    var error = new Model.Error()
                    {
                        Message = ex.Message,
                        Date = DateTime.Now.ToString(),
                    };
                    
                    if (db != null)
                    {
                        db.NewTerminalError.Add(error);
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("db is null");
                    }
                }
                else
                {
                    var error = new Model.Error()
                    {
                        Message = "پکت نامعتبر",
                        Date = DateTime.Now.ToString(),
                    };
                    
                    if (db != null)
                    {
                        db.NewTerminalError.Add(error);
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("db is null");
                    }
                };
                return DateTime.Now.ToString();
            }
            
        }
    }
}
