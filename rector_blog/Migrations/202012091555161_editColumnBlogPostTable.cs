namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editColumnBlogPostTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPostsModels", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.BlogPostsModels", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String());
            AlterColumn("dbo.BlogPostsModels", "Content", c => c.String());
            AlterColumn("dbo.BlogPostsModels", "Title", c => c.String());
        }
    }
}
