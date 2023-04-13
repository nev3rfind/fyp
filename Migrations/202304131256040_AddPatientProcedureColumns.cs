namespace NhsImsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPatientProcedureColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientProcedures", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientProcedures", "ActionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientProcedures", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientProcedures", "Status");
            DropColumn("dbo.PatientProcedures", "ActionDate");
            DropColumn("dbo.PatientProcedures", "CreatedDate");
        }
    }
}
