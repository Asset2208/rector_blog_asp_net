using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace rector_blog.Models
{
    public class StudModels
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual StudAddressModels StudAddressModels { get; set; }

    }

    //public class StudContext : DbContext
    //{
    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<StudModels>()
    //            .HasOptional(s => s.StudAddressModels) 
    //            .WithRequired(ad => ad.StudModels);
    //    }

    //    public DbSet<StudModels> StudModel { get; set; }
    //    public DbSet<StudAddressModels> StudAddressModel { get; set; }
    //}
}