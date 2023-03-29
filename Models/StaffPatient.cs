using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class StaffPatient
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int PatientId { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Patient Patient { get; set; }
    }
}