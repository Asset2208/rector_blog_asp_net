using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rector_blog.Models
{
    public class StudAddressModels
    {
        [ForeignKey("StudModels")]
        public int ID { get; set; }
        public string Address { get; set; }
        public virtual StudModels StudModels { get; set; }
    }

    //public class StudAddressContext : DbContext
    //{
    //    public DbSet<StudAddressModels> StudAddressModel { get; set; }

    //    public System.Data.Entity.DbSet<rector_blog.Models.StudModels> StudModels { get; set; }
    //}
}