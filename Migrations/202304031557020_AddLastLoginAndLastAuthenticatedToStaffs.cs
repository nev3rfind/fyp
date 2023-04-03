namespace NhsImsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastLoginAndLastAuthenticatedToStaffs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "LastLogin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Staffs", "LastAuthenticated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "LastAuthenticated");
            DropColumn("dbo.Staffs", "LastLogin");
        }
    }
}
