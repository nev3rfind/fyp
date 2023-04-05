using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhsImsApp.Data;
using NhsImsApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace NhsImsApp.Seeders
{
    public static class ExaminationSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Examinations.Any())
            {
                var examinationTypes = new List<Examination>
                {
                    new Examination { ExaminationId = 1, ExaminationType = "Physical Examination" },
                    new Examination { ExaminationId = 2, ExaminationType = "Blood Test" },
                    new Examination { ExaminationId = 3, ExaminationType = "X-ray" },
                    new Examination { ExaminationId = 4, ExaminationType = "Ultrasound" },
                    new Examination { ExaminationId = 5, ExaminationType = "MRI Scan" },
                    new Examination { ExaminationId = 6, ExaminationType = "ECG/EKG" },
                };

                context.Examinations.AddRange(examinationTypes);
                context.SaveChanges();
            }
        }
    }
}