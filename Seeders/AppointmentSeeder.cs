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
    public class AppointmentSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Appointments.Any())
            {
                var appointmentStatuses = new[] { "Scheduled", "Cancelled", "Attended", "Not Attended" };

                var appointmentFaker = new Faker<Appointment>()
                    .RuleFor(a => a.AppointmentName, f => f.Lorem.Word())
                    .RuleFor(a => a.PatientId, f => f.Random.Int(200, 500))
                    .RuleFor(a => a.StaffId, f => f.Random.Int(210, 300))
                    .RuleFor(a => a.CreatedTime, DateTime.Now)
                    .RuleFor(a => a.AppointmentDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddYears(1)))
                    .RuleFor(a => a.Description, f => f.Lorem.Sentence())
                    .RuleFor(a => a.Status, f => f.PickRandom(appointmentStatuses))
                    .RuleFor(a => a.AttendanceConfirmed, (f, a) => a.Status == "Attended");

                var appointments = appointmentFaker.Generate(200);
                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}