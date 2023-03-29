namespace NhsImsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        AppointmentName = c.String(),
                        PatientId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        AppointmentDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Status = c.String(),
                        AttendanceConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Dob = c.DateTime(nullable: false),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientExaminations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        ExaminationId = c.Int(nullable: false),
                        ExaminationDate = c.DateTime(nullable: false),
                        Analysis = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examinations", t => t.ExaminationId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.ExaminationId);
            
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        ExaminationId = c.Int(nullable: false, identity: true),
                        ExaminationType = c.String(),
                    })
                .PrimaryKey(t => t.ExaminationId);
            
            CreateTable(
                "dbo.PatientMedications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrescriptionId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        MedicationId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        PrescriptionDate = c.DateTime(nullable: false),
                        IsRenewed = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medications", t => t.MedicationId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.MedicationId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        MedicationName = c.String(),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.PatientProcedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        ProcedureId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Procedures", t => t.ProcedureId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.ProcedureId);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        ProcedureId = c.Int(nullable: false, identity: true),
                        ProcedureName = c.String(),
                    })
                .PrimaryKey(t => t.ProcedureId);
            
            CreateTable(
                "dbo.StaffPatients1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Username = c.String(),
                        IsDoctor = c.Boolean(nullable: false),
                        IsNurse = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffOtps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StaffId = c.Int(nullable: false),
                        Otp = c.Int(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        RequestedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.StaffPatients",
                c => new
                    {
                        StaffId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffId, t.PatientId })
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.StaffId)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.StaffPatients1", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffOtps", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffPatients", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.StaffPatients", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffPatients1", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientProcedures", "ProcedureId", "dbo.Procedures");
            DropForeignKey("dbo.PatientProcedures", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientMedications", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientMedications", "MedicationId", "dbo.Medications");
            DropForeignKey("dbo.PatientExaminations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientExaminations", "ExaminationId", "dbo.Examinations");
            DropIndex("dbo.StaffPatients", new[] { "PatientId" });
            DropIndex("dbo.StaffPatients", new[] { "StaffId" });
            DropIndex("dbo.StaffOtps", new[] { "StaffId" });
            DropIndex("dbo.StaffPatients1", new[] { "PatientId" });
            DropIndex("dbo.StaffPatients1", new[] { "StaffId" });
            DropIndex("dbo.PatientProcedures", new[] { "ProcedureId" });
            DropIndex("dbo.PatientProcedures", new[] { "PatientId" });
            DropIndex("dbo.PatientMedications", new[] { "MedicationId" });
            DropIndex("dbo.PatientMedications", new[] { "PatientId" });
            DropIndex("dbo.PatientExaminations", new[] { "ExaminationId" });
            DropIndex("dbo.PatientExaminations", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "StaffId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.StaffPatients");
            DropTable("dbo.StaffOtps");
            DropTable("dbo.Staffs");
            DropTable("dbo.StaffPatients1");
            DropTable("dbo.Procedures");
            DropTable("dbo.PatientProcedures");
            DropTable("dbo.Medications");
            DropTable("dbo.PatientMedications");
            DropTable("dbo.Examinations");
            DropTable("dbo.PatientExaminations");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
        }
    }
}
