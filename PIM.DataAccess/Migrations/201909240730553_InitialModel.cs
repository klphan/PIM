namespace PIM.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Visa = c.String(nullable: false, maxLength: 3),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDay = c.DateTime(nullable: false),
                        Version = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Version = c.Binary(),
                        GroupLeader_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.GroupLeader_ID)
                .Index(t => t.GroupLeader_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProjectNumber = c.Decimal(nullable: false, precision: 4, scale: 0),
                        Name = c.String(nullable: false, maxLength: 50),
                        Customer = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Version = c.Binary(),
                        Group_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.Group_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Employee_ID = c.Guid(nullable: false),
                        Project_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employee_ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Employee_ID)
                .Index(t => t.Project_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectEmployees", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmployees", "Employee_ID", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "GroupLeader_ID", "dbo.Employees");
            DropIndex("dbo.ProjectEmployees", new[] { "Project_ID" });
            DropIndex("dbo.ProjectEmployees", new[] { "Employee_ID" });
            DropIndex("dbo.Projects", new[] { "Group_ID" });
            DropIndex("dbo.Groups", new[] { "GroupLeader_ID" });
            DropTable("dbo.ProjectEmployees");
            DropTable("dbo.Projects");
            DropTable("dbo.Groups");
            DropTable("dbo.Employees");
        }
    }
}
