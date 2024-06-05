using Microsoft.EntityFrameworkCore;
using ScheduleAlarmCheck;

namespace ScheduleService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Alarm> Alarms { get; set; }
    }
}
