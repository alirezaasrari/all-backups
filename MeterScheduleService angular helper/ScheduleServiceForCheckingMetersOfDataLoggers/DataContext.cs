using Microsoft.EntityFrameworkCore;
using ScheduleServiceForCheckingMetersOfDataLoggers;

namespace SchduleServiceForCheckingMetersOfDataloggers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<DataLoggers> DataLogger { get; set; }
        public DbSet<DataLoggerWaterMeter> DataLoggerWaterMeter { get; set; }
        public DbSet<Log> NewTerminal { get; set; }
    }
}
