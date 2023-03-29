using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Examination
    {
        public int ExaminationId { get; set; }
        public string ExaminationType { get; set; }

        public virtual ICollection<PatientExamination> PatientExamination { get; set; }
    }
}