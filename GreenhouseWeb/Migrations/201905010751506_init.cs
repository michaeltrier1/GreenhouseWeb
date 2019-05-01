namespace GreenhouseWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Datalogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Greenhouse_ID = c.String(),
                        TimeOfReading = c.DateTime(nullable: false),
                        InternalTemperature = c.Single(nullable: false),
                        ExternalTemperature = c.Single(nullable: false),
                        Humidity = c.Single(nullable: false),
                        Waterlevel = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Greenhouses",
                c => new
                    {
                        GreenhouseID = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        IP = c.String(),
                        Port = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GreenhouseID);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ScheduleID = c.String(),
                        Blocknumber = c.Int(nullable: false),
                        InternalTemperature = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                        WaterLevel = c.Double(nullable: false),
                        RedLight = c.Double(nullable: false),
                        BlueLight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserGreenhouses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        GreenhouseID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserGreenhouses");
            DropTable("dbo.Schedules");
            DropTable("dbo.Greenhouses");
            DropTable("dbo.Datalogs");
        }
    }
}
