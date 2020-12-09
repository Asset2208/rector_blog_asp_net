using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rector_blog.Models
{
    public class QuestionBlogPostModels : IValidatableObject
    {
        [Key, ForeignKey("QuestionModels")]
        public int ID { get; set; }
        public int Views { get; set; }
        public DateTime Created_date { get; set; }
        public string Answer { get; set; }

        public virtual QuestionModels QuestionModels { get; set; }

        public virtual ICollection<QuestionCategoryModels> QuestionCategories { get; set; }
        public QuestionBlogPostModels()
        {
            QuestionCategories = new List<QuestionCategoryModels>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Answer))
            {
                errors.Add(new ValidationResult("Введите ответ"));
            }

            return errors;
        }
    }


}