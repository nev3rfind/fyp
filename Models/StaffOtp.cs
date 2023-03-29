using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class StaffOtp
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int Otp { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime RequestedDate { get; set; }

        public virtual Staff Staff { get; set; }

    }
}