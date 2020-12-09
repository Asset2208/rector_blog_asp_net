using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rector_blog.Models
{
    public class QuestionCategoryModels : IValidatableObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created_date { get; set; }
        public virtual ICollection<QuestionBlogPostModels> QuestionBlogPosts { get; set; }

        public QuestionCategoryModels()
        {
            QuestionBlogPosts = new List<QuestionBlogPostModels>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                errors.Add(new ValidationResult("Введите название"));
            }

            return errors;
        }
    }

}