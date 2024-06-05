using Microsoft.EntityFrameworkCore;

namespace CommandSchedule
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<CommandQueue> CommandQueue { get; set; }
    }
}
