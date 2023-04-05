using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhsImsApp.Data;
using NhsImsApp.Models;
using System.Security.Cryptography;
using System.Text;
using Bogus;

namespace NhsImsApp.Seeders
{
    public static class MedicationSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Medications.Any())
            {
                var medicationFaker = new Faker<Medication>()
                    .RuleFor(m => m.MedicationName, f => GenerateRandomMedicationName(f.Random));

                var medications = medicationFaker.Generate(50);
                context.Medications.AddRange(medications);
                context.SaveChanges();
            }
        }

        private static string GenerateRandomMedicationName(Randomizer randomizer)
        {
            var medicationPrefixes = new[]
            {
                "Amlo", "Hydro", "Metro", "Doxi", "Cele", "Simva", "Ator", "Carba", "Cyclo", "Pento"
            };

            var medicationSuffixes = new[]
            {
                "pril", "chlor", "statin", "zole", "mide", "dine", "alol", "flox", "cet", "vir"
            };

            return randomizer.ArrayElement(medicationPrefixes) + randomizer.ArrayElement(medicationSuffixes);
        }
    }
}