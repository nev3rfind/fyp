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
                var startMonthDate = DateTime.Now.AddMonths(-14);
                var endMonthDate = DateTime.Now.AddMonths(2);

                var appointmentFaker = new Faker<Appointment>()
                    .RuleFor(a => a.AppointmentName, f => f.Name.JobTitle())
                    .RuleFor(a => a.PatientId, f => f.Random.Int(201, 500))
                    .RuleFor(a => a.CreatedTime, DateTime.Now)
                    .RuleFor(a => a.Description, f => f.Lorem.Sentence())
                    .RuleFor(a => a.Status, f => f.PickRandom(appointmentStatuses))
                    .RuleFor(a => a.AttendanceConfirmed, (f, a) => a.Status == "Attended");

                var staffIds = Enumerable.Range(1, 7).ToList(); // Staff IDs from 1 to 8
                var random = new Random();
                var appointments = new List<Appointment>(10 * staffIds.Count); // Preallocate memory for appointments

                foreach (int staffId in staffIds)
                {
                    var currentDate = startMonthDate;

                    while (currentDate <= endMonthDate)
                    {
                        var appointmentCount = random.Next(5, 51); // Random appointment count per month between 5 and 50

                        for (int i = 0; i < appointmentCount; i++)
                        {
                            var appointment = appointmentFaker
                                .RuleFor(a => a.StaffId, staffId)
                                .RuleFor(a => a.AppointmentDate, f => f.Date.Between(currentDate, currentDate.AddMonths(1).AddDays(-1)))
                                .Generate();
                            appointments.Add(appointment);
                        }

                        currentDate = currentDate.AddMonths(1);
                    }
                }

                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}
