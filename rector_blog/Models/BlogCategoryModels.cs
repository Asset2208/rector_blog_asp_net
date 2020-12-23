using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace rector_blog.Models
{
    public class BlogCategoryModels : IValidatableObject
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Имя категории")]
        public string Name { get; set; }

        [Display(Name = "Активен ли?")]
        public bool Enabled { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime Created_date { get; set; }

        public virtual ICollection<BlogPostsModels> BlogPosts { get; set; }
        public BlogCategoryModels()
        {
            BlogPosts = new List<BlogPostsModels>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Введите название категории"));
            }

            return errors;
        }
    }

    
}