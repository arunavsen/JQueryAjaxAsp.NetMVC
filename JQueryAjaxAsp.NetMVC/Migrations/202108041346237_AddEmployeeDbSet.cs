namespace JQueryAjaxAsp.NetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.String(),
                        Office = c.String(),
                        Salary = c.Int(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
