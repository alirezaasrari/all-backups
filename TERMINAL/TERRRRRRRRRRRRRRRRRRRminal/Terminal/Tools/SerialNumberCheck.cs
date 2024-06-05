using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Context;

namespace Terminal.Tools
{
    public static class SerialNumberCheck
    {
        public static int SerialCheck(string number)
        {
            var services = new ServiceCollection();
            var connectionString = "server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;";
            services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetService<DataContext>();
            try
            {
                if (db.DataLogger.Any(l => l.DataLogger == number && l.DeletedAt == null))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 0;
            }
            
        }
    }
}
