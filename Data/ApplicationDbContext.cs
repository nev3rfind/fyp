using System.Data.Entity;
using NhsImsApp.Models;

namespace NhsImsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<StaffPatient> StaffPatients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<PatientMedication> PatientMedications { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<PatientProcedure> PatientProcedures { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<PatientExamination> PatientExaminations { get; set; }
        public DbSet<StaffOtp> StaffOtps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Staff to Patients (many-to-many relationship)
            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Patients)
                .WithMany(p => p.Staffs)
                .Map(cs =>
                {
                    cs.MapLeftKey("StaffId");
                    cs.MapRightKey("PatientId");
                    cs.ToTable("StaffPatients");
                });
            // Staff to Appointments (one-to-many relationship)
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Staff)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StaffId);
            // Patients to Appointments (one-to-many relationship)
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);
            // Patients to PatientMedications (one-to-many relationship)
            modelBuilder.Entity<PatientMedication>()
                .HasRequired(pm => pm.Patient)
                .WithMany(p => p.PatientMedications)
                .HasForeignKey(pm => pm.PatientId);
            // Medication to PatientMedications (one-to-many relationship)
            modelBuilder.Entity<PatientMedication>()
                .HasRequired(pm => pm.Medication)
                .WithMany(m => m.PatientMedication)
                .HasForeignKey(pm => pm.MedicationId);
            // Patients to PatientProcedures (one-to-many relationship)
            modelBuilder.Entity<PatientProcedure>()
                .HasRequired(pp => pp.Patient)
                .WithMany(p => p.PatientProcedures)
                .HasForeignKey(pp => pp.PatientId);
            // Procedures to PatientProcedures (one-to-many relationship)
            modelBuilder.Entity<PatientProcedure>()
                .HasRequired(pp => pp.Procedure)
                .WithMany(pr => pr.PatientProcedures)
                .HasForeignKey(pp => pp.ProcedureId);
            // Patients to PatientExamination (one-to-many relationship)
            modelBuilder.Entity<PatientExamination>()
                .HasRequired(pe => pe.Patient)
                .WithMany(p => p.PatientExaminations)
                .HasForeignKey(pe => pe.PatientId);
            // Examinations to PatientExamination (one-to-many relationship)
            modelBuilder.Entity<PatientExamination>()
                .HasRequired(pe => pe.Examination)
                .WithMany(e => e.PatientExamination)
                .HasForeignKey(pe => pe.ExaminationId);
            // Staff to StaffOtps (one-to-many relationship)
            modelBuilder.Entity<StaffOtp>()
                .HasRequired(so => so.Staff)
                .WithMany(s => s.StaffOtps)
                .HasForeignKey(so => so.StaffId);

            base.OnModelCreating(modelBuilder);
        }
    }
}