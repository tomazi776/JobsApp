namespace DataLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FailedCounter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        DoAfter = c.DateTime(),
                        LastUpdatedAt = c.DateTime(),
                        FailedCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        JobId = c.Guid(nullable: false),
                        Job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .Index(t => t.Job_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.Logs", new[] { "Job_Id" });
            DropIndex("dbo.Jobs", new[] { "Name" });
            DropTable("dbo.Logs");
            DropTable("dbo.Jobs");
        }
    }
}
