namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String(nullable: false));
        }
    }
}
