using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public String Username {get; set; }
        public bool IsDoctor { get; set; }
        public bool IsNurse { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastAuthenticated { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<StaffPatient> StaffPatients { get; set; }
        public virtual ICollection<StaffOtp> StaffOtps { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}