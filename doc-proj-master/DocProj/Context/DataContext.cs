using DocProj.Model;
using Microsoft.EntityFrameworkCore;

namespace DocProj.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ListOfOption> ListOfOptions { get; set; }
        public DbSet<OptionDetail> OptionDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OptionDetail>()
                .HasOne(o => o.ListOfOption)
                .WithMany(u => u.OptionDetail)
                .HasForeignKey(o => o.ListOfOptionId);
        }
    }
}
