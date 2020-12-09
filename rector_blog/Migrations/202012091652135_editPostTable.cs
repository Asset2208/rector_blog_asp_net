namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPostTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogPostsModels", "ImageUrl", c => c.String());
        }
    }
}
