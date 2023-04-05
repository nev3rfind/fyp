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
    public static class PatientMedicationSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.PatientMedications.Any())
            {
                var patientMedicationFaker = new Faker<PatientMedication>()
                    .RuleFor(pm => pm.PatientId, f => f.Random.Number(205, 500))
                    .RuleFor(pm => pm.MedicationId, f => f.Random.Number(1, 50))
                    .RuleFor(pm => pm.StaffId, f => f.Random.Number(205, 300))
                    .RuleFor(pm => pm.StartDate, f => f.Date.Between(DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(2)))
                    .RuleFor(pm => pm.EndDate, (f, pm) => pm.StartDate.AddMonths(1))
                    .RuleFor(pm => pm.PrescriptionDate, f => DateTime.Now)
                    .RuleFor(pm => pm.IsRenewed, f => f.Random.Bool());

                var patientMedications = patientMedicationFaker.Generate(400);
                context.PatientMedications.AddRange(patientMedications);
                context.SaveChanges();
            }
        }
    }
}