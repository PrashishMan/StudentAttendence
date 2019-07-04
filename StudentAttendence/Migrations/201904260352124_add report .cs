namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendences", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Attendences", "TimetableID", "dbo.Timetables");
            DropIndex("dbo.Attendences", new[] { "StudentID" });
            DropIndex("dbo.Attendences", new[] { "TimetableID" });
            DropTable("dbo.Attendences");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        AttendenceID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        TimetableID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Condition = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttendenceID);
            
            CreateIndex("dbo.Attendences", "TimetableID");
            CreateIndex("dbo.Attendences", "StudentID");
            AddForeignKey("dbo.Attendences", "TimetableID", "dbo.Timetables", "TimeTableId", cascadeDelete: true);
            AddForeignKey("dbo.Attendences", "StudentID", "dbo.Students", "StudentID", cascadeDelete: true);
        }
    }
}
