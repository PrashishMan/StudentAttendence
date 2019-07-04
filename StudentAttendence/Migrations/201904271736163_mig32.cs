namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig32 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "FacultyID", "dbo.Faculties");
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupTimetables", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Timetables", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.Timetables", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.GroupTimetables", "TimetableID", "dbo.Timetables");
            DropIndex("dbo.GroupTimetables", new[] { "GroupID" });
            DropIndex("dbo.GroupTimetables", new[] { "TimetableID" });
            DropIndex("dbo.Groups", new[] { "FacultyID" });
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropIndex("dbo.Timetables", new[] { "ModuleID" });
            DropIndex("dbo.Timetables", new[] { "SemesterID" });
            DropTable("dbo.GroupTimetables");
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.Timetables");
            DropTable("dbo.Semesters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterID = c.Int(nullable: false, identity: true),
                        SemesterStartDate = c.DateTime(nullable: false),
                        SemesterEndDate = c.DateTime(nullable: false),
                        SemesterNo = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SemesterID);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        TimeTableId = c.Int(nullable: false, identity: true),
                        ClassStartTime = c.Time(nullable: false, precision: 7),
                        ClassEndTime = c.Time(nullable: false, precision: 7),
                        Day = c.String(nullable: false),
                        Room = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Year = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        SemesterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimeTableId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Contact = c.String(nullable: false, maxLength: 100),
                        EnrolledDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        GroupID = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        FacultyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.GroupTimetables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.String(maxLength: 10),
                        TimetableID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Timetables", "SemesterID");
            CreateIndex("dbo.Timetables", "ModuleID");
            CreateIndex("dbo.Students", "GroupID");
            CreateIndex("dbo.Groups", "FacultyID");
            CreateIndex("dbo.GroupTimetables", "TimetableID");
            CreateIndex("dbo.GroupTimetables", "GroupID");
            AddForeignKey("dbo.GroupTimetables", "TimetableID", "dbo.Timetables", "TimeTableId", cascadeDelete: true);
            AddForeignKey("dbo.Timetables", "SemesterID", "dbo.Semesters", "SemesterID", cascadeDelete: true);
            AddForeignKey("dbo.Timetables", "ModuleID", "dbo.Modules", "ModuleID", cascadeDelete: true);
            AddForeignKey("dbo.GroupTimetables", "GroupID", "dbo.Groups", "GroupID");
            AddForeignKey("dbo.Students", "GroupID", "dbo.Groups", "GroupID");
            AddForeignKey("dbo.Groups", "FacultyID", "dbo.Faculties", "FacultyID", cascadeDelete: true);
        }
    }
}
