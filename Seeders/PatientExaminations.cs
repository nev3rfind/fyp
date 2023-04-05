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
    public static class PatientExaminations
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.PatientExaminations.Any())
            {
                var patientExaminationsFaker = new Faker<PatientExamination>()
                    .RuleFor(pe => pe.PatientId, f => f.Random.Number(201, 500))
                    .RuleFor(pe => pe.ExaminationId, f => f.Random.Number(1, 6))
                    .RuleFor(pe => pe.ExaminationDate, f => f.Date.Between(DateTime.Now.AddYears(-4), DateTime.Now))
                    .RuleFor(pe => pe.Analysis, f => f.Random.MedicalAnalysis());

                var patientExaminations = patientExaminationsFaker.Generate(100);
                context.PatientExaminations.AddRange(patientExaminations);
                context.SaveChanges();
            }
        }
    }
    public static class FakerExtensions
    {
        public static string MedicalAnalysis(this Randomizer randomizer)
        {
            var medicalAnalysisList = new[]
            {
            "Normal blood pressure",
            "Low hemoglobin levels",
            "High cholesterol levels",
            "Signs of inflammation",
            "Normal heart function",
            "No abnormalities detected",
            "Mild degenerative changes",
            "Possible infection",
            "Consistent with allergies",
            "Elevated liver enzymes",
        };

            return randomizer.ArrayElement(medicalAnalysisList);
        }
    }
}