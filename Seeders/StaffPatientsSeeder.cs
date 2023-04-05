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
    public class StaffPatientsSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.StaffPatients.Any())
            {
                var staffIds = context.Staffs.Select(s => s.StaffId).ToList();
                var patientIds = context.Patients.Where(p => p.PatientId >= 200)
                                                 .Select(p => p.PatientId)
                                                 .ToList();

                var rng = new Random();

                foreach (var staffId in staffIds)
                {
                    int numberOfPatients = rng.Next(4, 41);

                    for (int i = 0; i < numberOfPatients; i++)
                    {
                        var randomPatientId = patientIds[rng.Next(patientIds.Count)];

                        var staffPatient = new StaffPatient
                        {
                            StaffId = staffId,
                            PatientId = randomPatientId
                        };

                        context.StaffPatients.Add(staffPatient);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}