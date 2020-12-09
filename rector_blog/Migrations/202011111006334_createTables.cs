namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategoryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BlogPostsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created_date = c.DateTime(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        ImageUrl = c.String(),
                        Views = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        Comments_enabled = c.Boolean(nullable: false),
                        BlogCategoryModelsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategoryModels", t => t.BlogCategoryModelsId)
                .Index(t => t.BlogCategoryModelsId);
            
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Is_reply_to_id = c.Int(nullable: false),
                        Content = c.String(),
                        User_id = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                        BlogPostModelsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPostsModels", t => t.BlogPostModelsId)
                .Index(t => t.BlogPostModelsId);
            
            CreateTable(
                "dbo.QuestionBlogPostModels",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionModels", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.QuestionCategoryModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        User_id = c.String(),
                        Is_answered = c.Boolean(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudAddressModels",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StudModels", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.StudModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionCategoryModelsQuestionBlogPostModels",
                c => new
                    {
                        QuestionCategoryModels_ID = c.Int(nullable: false),
                        QuestionBlogPostModels_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionCategoryModels_ID, t.QuestionBlogPostModels_ID })
                .ForeignKey("dbo.QuestionCategoryModels", t => t.QuestionCategoryModels_ID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionBlogPostModels", t => t.QuestionBlogPostModels_ID, cascadeDelete: true)
                .Index(t => t.QuestionCategoryModels_ID)
                .Index(t => t.QuestionBlogPostModels_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudAddressModels", "ID", "dbo.StudModels");
            DropForeignKey("dbo.QuestionBlogPostModels", "ID", "dbo.QuestionModels");
            DropForeignKey("dbo.QuestionCategoryModelsQuestionBlogPostModels", "QuestionBlogPostModels_ID", "dbo.QuestionBlogPostModels");
            DropForeignKey("dbo.QuestionCategoryModelsQuestionBlogPostModels", "QuestionCategoryModels_ID", "dbo.QuestionCategoryModels");
            DropForeignKey("dbo.CommentModels", "BlogPostModelsId", "dbo.BlogPostsModels");
            DropForeignKey("dbo.BlogPostsModels", "BlogCategoryModelsId", "dbo.BlogCategoryModels");
            DropIndex("dbo.QuestionCategoryModelsQuestionBlogPostModels", new[] { "QuestionBlogPostModels_ID" });
            DropIndex("dbo.QuestionCategoryModelsQuestionBlogPostModels", new[] { "QuestionCategoryModels_ID" });
            DropIndex("dbo.StudAddressModels", new[] { "ID" });
            DropIndex("dbo.QuestionBlogPostModels", new[] { "ID" });
            DropIndex("dbo.CommentModels", new[] { "BlogPostModelsId" });
            DropIndex("dbo.BlogPostsModels", new[] { "BlogCategoryModelsId" });
            DropTable("dbo.QuestionCategoryModelsQuestionBlogPostModels");
            DropTable("dbo.StudentModels");
            DropTable("dbo.StudModels");
            DropTable("dbo.StudAddressModels");
            DropTable("dbo.QuestionModels");
            DropTable("dbo.QuestionCategoryModels");
            DropTable("dbo.QuestionBlogPostModels");
            DropTable("dbo.CommentModels");
            DropTable("dbo.BlogPostsModels");
            DropTable("dbo.BlogCategoryModels");
        }
    }
}
