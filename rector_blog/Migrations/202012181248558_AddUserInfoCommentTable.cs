namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserInfoCommentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CommentModels", "ApplicationUser_Id");
            AddForeignKey("dbo.CommentModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.CommentModels", "ApplicationUser_Id");
        }
    }
}
