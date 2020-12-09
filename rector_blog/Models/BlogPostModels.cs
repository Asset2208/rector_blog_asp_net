using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace rector_blog.Models
{
    public class BlogPostsModels
    {
        public int Id { get; set; }
        public DateTime Created_date { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [AllowHtml]
        [Required]
        [Display(Name = "Содержание")]
        public string Content { get; set; }

        
        [Required]
        [Display(Name = "Ссылка на фотографию")]
        [Remote("IsURLExist", "BlogPosts", HttpMethod = "POST", ErrorMessage = "The image is not exist by this URL")]
        public string ImageUrl { get; set; }
        public int Views { get; set; }
        public bool Enabled { get; set; }
        public bool Comments_enabled { get; set; }
        public int? BlogCategoryModelsId { get; set; }
        public BlogCategoryModels BlogCategoryModels { get; set; }


        public ICollection<CommentModels> Comments { get; set; }
        public BlogPostsModels()
        {
            Comments = new List<CommentModels>();
        }

    }

}