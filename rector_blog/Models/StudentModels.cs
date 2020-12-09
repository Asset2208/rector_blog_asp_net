using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rector_blog.Models
{
    public class StudentModels
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
    //public class StudentContext : DbContext
    //{
    //    public DbSet<StudentModels> StudentModel { get; set; }
    //}
}