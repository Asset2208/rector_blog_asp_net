namespace rector_blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStudentNameColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudModels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudModels", "Name", c => c.String());
        }
    }
}
