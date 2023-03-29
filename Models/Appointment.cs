using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string AppointmentName { get; set; }
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool AttendanceConfirmed { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Staff Staff { get; set; }
    }
}