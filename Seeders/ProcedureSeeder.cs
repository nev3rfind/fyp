using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhsImsApp.Data;
using NhsImsApp.Models;
using Bogus;
using System.Security.Cryptography;
using System.Text;

namespace NhsImsApp.Seeders
{
    public static class ProcedureSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Procedures.Any())
            {
                var medicalProcedures = new List<Procedure>
                {
                    new Procedure { ProcedureId = 1, ProcedureName = "Appendectomy" },
                    new Procedure { ProcedureId = 2, ProcedureName = "Cataract Surgery" },
                    new Procedure { ProcedureId = 3, ProcedureName = "Cholecystectomy" },
                    new Procedure { ProcedureId = 4, ProcedureName = "Coronary Bypass" },
                    new Procedure { ProcedureId = 5, ProcedureName = "Hip Replacement" },
                    new Procedure { ProcedureId = 6, ProcedureName = "Hysterectomy" },
                    new Procedure { ProcedureId = 7, ProcedureName = "Knee Replacement" },
                    new Procedure { ProcedureId = 8, ProcedureName = "Mastectomy" },
                    new Procedure { ProcedureId = 9, ProcedureName = "Nephrectomy" },
                    new Procedure { ProcedureId = 10, ProcedureName = "Prostatectomy" },
                    new Procedure { ProcedureId = 11, ProcedureName = "Spinal Fusion" },
                    new Procedure { ProcedureId = 12, ProcedureName = "Thyroidectomy" },
                    new Procedure { ProcedureId = 13, ProcedureName = "Tonsillectomy" },
                    new Procedure { ProcedureId = 14, ProcedureName = "Tubal Ligation" },
                    new Procedure { ProcedureId = 15, ProcedureName = "Vasectomy" },
                    new Procedure { ProcedureId = 16, ProcedureName = "Angioplasty" },
                };

                context.Procedures.AddRange(medicalProcedures);
                context.SaveChanges();
            }
        }
    }

}