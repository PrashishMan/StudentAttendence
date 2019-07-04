namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig33 : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        FacultyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Faculties", t => t.FacultyID, cascadeDelete: true)
                .Index(t => t.FacultyID);
            
            CreateTable(
                "dbo.GroupTimetables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.String(maxLength: 10),
                        TimetableID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .ForeignKey("dbo.Timetables", t => t.TimetableID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.TimetableID);
            
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
                .PrimaryKey(t => t.TimeTableId)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.ModuleID)
                .Index(t => t.SemesterID);
            
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
                "dbo.ModuleGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SemesterID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        GroupID = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.GroupID)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.SemesterID)
                .Index(t => t.ModuleID)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModuleGroups", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.ModuleGroups", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleGroups", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Attendences", "TimetableID", "dbo.Timetables");
            DropForeignKey("dbo.Attendences", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GroupTimetables", "TimetableID", "dbo.Timetables");
            DropForeignKey("dbo.Timetables", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Timetables", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.GroupTimetables", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "FacultyID", "dbo.Faculties");
            DropIndex("dbo.ModuleGroups", new[] { "GroupID" });
            DropIndex("dbo.ModuleGroups", new[] { "ModuleID" });
            DropIndex("dbo.ModuleGroups", new[] { "SemesterID" });
            DropIndex("dbo.Timetables", new[] { "SemesterID" });
            DropIndex("dbo.Timetables", new[] { "ModuleID" });
            DropIndex("dbo.GroupTimetables", new[] { "TimetableID" });
            DropIndex("dbo.GroupTimetables", new[] { "GroupID" });
            DropIndex("dbo.Groups", new[] { "FacultyID" });
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropIndex("dbo.Attendences", new[] { "TimetableID" });
            DropIndex("dbo.Attendences", new[] { "StudentID" });
            DropTable("dbo.ModuleGroups");
            DropTable("dbo.Semesters");
            DropTable("dbo.Timetables");
            DropTable("dbo.GroupTimetables");
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.Attendences");
        }
    }
}
