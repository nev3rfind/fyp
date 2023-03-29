using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhsImsApp.Models
{
    public class Procedure
    {
        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; }

        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }
    }
}