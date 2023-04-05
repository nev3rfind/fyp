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
    public static class PatientSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Patients.Any())
            {
                var patientFaker = new Faker<Patient>()
                    .RuleFor(p => p.PatientId, f => f.Random.Number(1000000, 9999999))
                    .RuleFor(p => p.FullName, f => f.Name.FullName())
                    .RuleFor(p => p.Dob, f => f.Date.Between(new DateTime(1923, 1, 1), DateTime.Now.AddYears(-1))) // Patients aged between 1 and 100 years old
                    .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "Male", "Female" }));

                var patients = patientFaker.Generate(700);
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}