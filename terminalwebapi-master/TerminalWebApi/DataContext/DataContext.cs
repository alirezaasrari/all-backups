using Microsoft.EntityFrameworkCore;
using TerminalWebApi.Model;

namespace TerminalWebApi
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<TerminalData> NewTerminal { get; set; }
        public DbSet<TerminalError> NewTerminalError { get; set; }
        public DbSet<TerminalIoError> NewTerminalIoError { get; set; }
        public DbSet<TerminalSocketError> NewTerminalSocketError { get; set; }
    }
}
