namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTestModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestModels");
        }
    }
}
