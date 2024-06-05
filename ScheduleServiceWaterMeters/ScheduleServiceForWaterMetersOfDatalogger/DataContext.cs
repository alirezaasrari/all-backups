using Microsoft.EntityFrameworkCore;

namespace ScheduleServiceForWaterMetersOfDatalogger
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<DataLoggers> DataLogger { get; set; }
        public DbSet<DataLoggerWaterMeter> DataLoggerWaterMeter { get; set; }
    }
}
