using Microsoft.EntityFrameworkCore;

namespace ScheduleService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<DataLoggers> DataLogger { get; set; }
    }
}
