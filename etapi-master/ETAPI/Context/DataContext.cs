using ETAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Form> FLW_tbl_Forms { get; set; }
        public DbSet<Unit> ET_tbl_Unit { get; set; }
        public DbSet<FormUnit> FLW_tbl_FormUnit { get; set; }
        public DbSet<FormChart> FLW_tbl_FormChart { get; set; }
        public DbSet<CodeFormx> FLW_tbl_CodeFormX { get; set; }
        public DbSet<Taeed> FLW_tbl_Taeed { get; set; }
        public DbSet<Variable> FLW_tbl_Variable { get; set; }

        //[DbSet(Schema = "order")]
        public DbSet<If> FLW_tbl_If { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormUnit>()
                .HasOne(o => o.Form)
                .WithMany(u => u.FormUnits)
                .HasForeignKey(o => o.ID_Form);
        }
    }
}
