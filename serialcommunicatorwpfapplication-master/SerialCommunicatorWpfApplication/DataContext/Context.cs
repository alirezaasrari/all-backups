using Microsoft.EntityFrameworkCore;
using SerialCommunicatorWpfApplication.Model;

namespace SerialCommunicatorWpfApplication.DataContext
{
    public class Context : DbContext
    {
        public DbSet<ProductionPanel> ProductionPanel { get; set; }
        public DbSet<TerminalDataModel> NewTerminal {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=185.7.212.184;port=3306;database=talayieh-test;user=root;password=1372328$oheiL!@#$!;");
        }
    }
}