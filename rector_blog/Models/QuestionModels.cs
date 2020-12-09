using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rector_blog.Models
{
    public class QuestionModels : IValidatableObject
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string User_id { get; set; }
        public bool Is_answered { get; set; }
        public DateTime Created_date { get; set; }
        public virtual QuestionBlogPostModels QuestionBlogPostModels { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Content))
            {
                errors.Add(new ValidationResult("Введите название"));
            }

            return errors;
        }
    }

}