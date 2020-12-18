namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editQuestionTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionModels", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.QuestionModels", "User_Id");
            AddForeignKey("dbo.QuestionModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.QuestionModels", new[] { "User_Id" });
            AlterColumn("dbo.QuestionModels", "User_Id", c => c.String());
        }
    }
}
