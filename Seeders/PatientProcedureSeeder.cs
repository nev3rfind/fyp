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
                    .RuleFor(pp => pp.Description, f => f.Lorem.Paragraph())
                    .RuleFor(pp => pp.CreatedDate, DateTime.Now)
                    .RuleFor(pp => pp.Status, f => f.PickRandom(new[] { "Scheduled", "Completed", "Cancelled" }))
                    .RuleFor(pp => pp.ActionDate, (f, pp) =>
                    {
                        DateTime currentDate = DateTime.Now;
                        DateTime actionDate;

                        switch (pp.Status)
                        {
                            case "Scheduled":
                                actionDate = f.Date.Between(currentDate, currentDate.AddMonths(4));
                                break;
                            case "Completed":
                                actionDate = f.Date.Between(currentDate.AddMonths(-6), currentDate);
                                break;
                            case "Cancelled":
                                actionDate = f.Date.Between(currentDate.AddYears(-1), currentDate.AddYears(1));
                                break;
                            default:
                                actionDate = currentDate;
                                break;
                        }

                        return actionDate;
                    });

                var patientProcedures = patientProcedureFaker.Generate(800);
                context.PatientProcedures.AddRange(patientProcedures);
                context.SaveChanges();
            }
        }
    }
}