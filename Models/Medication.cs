using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }

        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
    }
}