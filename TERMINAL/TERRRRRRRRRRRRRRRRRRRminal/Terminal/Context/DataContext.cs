using Microsoft.EntityFrameworkCore;
using Terminal.Model;

namespace Terminal.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Log> NewTerminal { get; set; }
        public DbSet<Error> NewTerminalError { get; set; }
        public DbSet<SocketErrors> NewTerminalSocketError { get; set; }
        public DbSet<IoError> NewTerminalIoError { get; set; }
        public DbSet<DataLoggers> DataLogger { get; set; }
        public DbSet<CommandQueue> CommandQueue { get; set; }
    }
}
