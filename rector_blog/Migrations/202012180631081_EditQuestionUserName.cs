namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditQuestionUserName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionModels", new[] { "User_Id" });
            AddColumn("dbo.QuestionModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuestionModels", "User_id", c => c.String());
            CreateIndex("dbo.QuestionModels", "ApplicationUser_Id");
            AddForeignKey("dbo.QuestionModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionModels", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.QuestionModels", "User_id", c => c.String(maxLength: 128));
            DropColumn("dbo.QuestionModels", "ApplicationUser_Id");
            CreateIndex("dbo.QuestionModels", "User_Id");
            AddForeignKey("dbo.QuestionModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
