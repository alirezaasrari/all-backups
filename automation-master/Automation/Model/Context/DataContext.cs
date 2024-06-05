using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Automation.Model.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        //[DbSet(Schema = "order")]
        //public DbSet<If> FLW_tbl_If { get; set; }
        public DbSet<RegisterUser> Users { get; set; }
        public DbSet<AutomationModel> TabelName { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<FormUnit>()
        //        .HasOne(o => o.Form)
        //        .WithMany(u => u.FormUnits)
        //        .HasForeignKey(o => o.ID_Form);
        //    //modelBuilder.HasDefaultSchema("my_schema");
        //}
    }
}
