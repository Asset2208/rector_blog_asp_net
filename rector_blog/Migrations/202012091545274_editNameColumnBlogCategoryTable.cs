namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editNameColumnBlogCategoryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogCategoryModels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogCategoryModels", "Name", c => c.String());
        }
    }
}
