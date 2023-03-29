using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NhsImsApp.Models
{
    public class PatientMedication
    {
        [Key]
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // maximum one month
        public DateTime PrescriptionDate { get; set; } // when prescription was created
        public bool? IsRenewed { get; set; } // if renewed true - creates new record, if false - do nothing 

        public virtual Patient Patient { get; set; }
        public virtual Medication Medication { get; set; }
    }
}