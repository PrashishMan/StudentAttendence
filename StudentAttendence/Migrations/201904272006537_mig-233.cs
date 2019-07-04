namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig233 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modules", "SemesterID", c => c.Int(nullable: false));
            CreateIndex("dbo.Modules", "SemesterID");
            AddForeignKey("dbo.Modules", "SemesterID", "dbo.Semesters", "SemesterID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "SemesterID", "dbo.Semesters");
            DropIndex("dbo.Modules", new[] { "SemesterID" });
            DropColumn("dbo.Modules", "SemesterID");
        }
    }
}
