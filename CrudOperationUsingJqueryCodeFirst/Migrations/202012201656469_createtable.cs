namespace CrudOperationUsingJqueryCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Dob = c.String(),
                        Image = c.String(),
                        Contect = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Stateid = c.Int(nullable: false, identity: true),
                        Statename = c.String(),
                    })
                .PrimaryKey(t => t.Stateid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "StateId", "dbo.States");
            DropForeignKey("dbo.Employees", "CityId", "dbo.Cities");
            DropIndex("dbo.Employees", new[] { "CityId" });
            DropIndex("dbo.Employees", new[] { "StateId" });
            DropTable("dbo.States");
            DropTable("dbo.Employees");
            DropTable("dbo.Cities");
        }
    }
}
