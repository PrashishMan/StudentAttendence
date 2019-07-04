namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModuleGroups", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.ModuleGroups", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleGroups", "SemesterID", "dbo.Semesters");
            DropIndex("dbo.ModuleGroups", new[] { "SemesterID" });
            DropIndex("dbo.ModuleGroups", new[] { "ModuleID" });
            DropIndex("dbo.ModuleGroups", new[] { "GroupID" });
            DropTable("dbo.ModuleGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ModuleGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SemesterID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        GroupID = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ModuleGroups", "GroupID");
            CreateIndex("dbo.ModuleGroups", "ModuleID");
            CreateIndex("dbo.ModuleGroups", "SemesterID");
            AddForeignKey("dbo.ModuleGroups", "SemesterID", "dbo.Semesters", "SemesterID", cascadeDelete: true);
            AddForeignKey("dbo.ModuleGroups", "ModuleID", "dbo.Modules", "ModuleID", cascadeDelete: true);
            AddForeignKey("dbo.ModuleGroups", "GroupID", "dbo.Groups", "GroupID");
        }
    }
}
