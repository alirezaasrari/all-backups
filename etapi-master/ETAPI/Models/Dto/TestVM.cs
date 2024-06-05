using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;

namespace ETAPI.Models.ViewModels
{
    public class TestVM
    {
        public string Name { get; set; } = string.Empty;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .HasOne(c => c.CustomerAddress)
        //        .WithOne(ca => ca.Customer)
        //        .HasForeignKey<CustomerAddress>(ca => ca.CustomerId)
        //        .IsRequired();
        //}



//        SELECT vpakn.C_kala

//      , sum(vpakn.baghimande) AS baghimande
//FROM dbo.vina_paya_asg_darkhastKharidN vpakn
//WHERE vpakn.C_kala = ''
//GROUP BY vpakn.C_kala

    }
}
