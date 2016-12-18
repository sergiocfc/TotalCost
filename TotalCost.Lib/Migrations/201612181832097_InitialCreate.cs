namespace TotalCost.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Sum = c.Double(nullable: false),
                        Note = c.String(),
                        Bill_Id = c.Int(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.Bill_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Bill_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                        Type = c.Int(nullable: false),
                        Limit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Payments", "Bill_Id", "dbo.Bills");
            DropIndex("dbo.Payments", new[] { "Group_Id" });
            DropIndex("dbo.Payments", new[] { "Bill_Id" });
            DropTable("dbo.Groups");
            DropTable("dbo.Payments");
            DropTable("dbo.Bills");
        }
    }
}
