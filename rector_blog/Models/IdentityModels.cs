using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace rector_blog.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BlogCategoryModels> BlogCategoryModel { get; set; }
        public DbSet<CommentModels> CommentModel { get; set; }
        public DbSet<BlogPostsModels> BlogPostModel { get; set; }
        public DbSet<QuestionBlogPostModels> QuestionBlogPostModel { get; set; }
        public DbSet<QuestionCategoryModels> QuestionCategoryModel { get; set; }
        public DbSet<QuestionModels> QuestionModel { get; set; }
        public DbSet<StudAddressModels> StudAddressModel { get; set; }
        public DbSet<StudentModels> StudentModel { get; set; }
        public DbSet<StudModels> StudModel { get; set; }
        public DbSet<TestModels> TestModel { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}