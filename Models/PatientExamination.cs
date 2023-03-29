using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class PatientExamination
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ExaminationId { get; set; }
        public DateTime ExaminationDate { get; set; }
        public string Analysis { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Examination Examination { get; set; }
    }
}