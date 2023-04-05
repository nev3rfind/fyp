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
    public static class PatientProcedureSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.PatientProcedures.Any())
            {
                var patientProcedureFaker = new Faker<PatientProcedure>()
                    .RuleFor(pp => pp.PatientId, f => f.Random.Int(205, 500))
                    .RuleFor(pp => pp.ProcedureId, f => f.Random.Int(1, 16))
                    .RuleFor(pp => pp.Description, f => f.Lorem.Paragraph());

                var patientProcedures = patientProcedureFaker.Generate(200);
                context.PatientProcedures.AddRange(patientProcedures);
                context.SaveChanges();
            }
        }
    }
}