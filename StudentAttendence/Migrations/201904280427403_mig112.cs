namespace StudentAttendence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig112 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timetables", "ClassType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timetables", "ClassType");
        }
    }
}
