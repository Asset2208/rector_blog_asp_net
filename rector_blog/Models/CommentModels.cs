using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rector_blog.Models
{
    public class CommentModels : IValidatableObject
    {
        public int Id { get; set; }
        public int Is_reply_to_id { get; set; }
        public string Content { get; set; }
        public string User_id { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created_date { get; set; }
        public int? BlogPostModelsId { get; set; }
        public BlogPostsModels BlogPostModels { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(this.Content))
            {
                errors.Add(new ValidationResult("Введите содержание"));
            }

            return errors;
        }
    }
    
}