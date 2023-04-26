using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientMedication> PatientMedications { get; set; }
        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }
        public virtual ICollection<PatientExamination> PatientExaminations { get; set; }
        public virtual ICollection<StaffPatient> StaffPatients { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    public class PatientMedicationInputModel
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PatientProcedureInputModel
    {
        public int PatientId { get; set; }
        public int ProcedureId { get; set; }
        public DateTime ProcedureDate { get; set; }
        public string ProcedureDescription { get; set; }
    }

    public class PatientExaminationInputModel
    {
        public int PatientId { get; set; }
        public int ExaminationId { get; set; }
        public DateTime ExaminationDate { get; set; }
        public string ExaminationAnalysis { get; set; }
    }
}