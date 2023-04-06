using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhsImsApp.Models;
using NhsImsApp.Data;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Data.Entity;

namespace NhsImsApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public AppointmentController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        ///  Gets appointments grouped by a mounth and count the total appointments in each month
        /// </summary>
        /// <returns>Grouped appointments</returns>
        [HttpPost]
        public ActionResult GetAppointmentsByStaffId(int staffId)
        {
            var now = DateTime.UtcNow;
            var twelveMonthsAgo = now.AddMonths(-12);

            var appointments = _Context.Appointments
                .Where(a => a.StaffId == staffId && a.AppointmentDate >= twelveMonthsAgo && a.AppointmentDate <= now)
                .ToList();

            if (appointments.Count > 0)
            {
                var groupedAppointments = appointments
                    .GroupBy(a => new { a.AppointmentDate.Year, a.AppointmentDate.Month })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Month)
                    .Select(g => new
                    {
                        Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month),
                        Count = g.Count()
                    }).ToList();

                return Json(new
                {
                    success = true,
                    groupedAppointments = groupedAppointments

                });
            }

            return Json(new
            {
                success = false

            });
                
        }

        [HttpPost]
        public ActionResult GetUpcomingAppointmentsByStaffId(int staffId)
        {
            var now = DateTime.UtcNow;

            var appointments = _Context.Appointments
                .Where(a => a.StaffId == staffId && a.AppointmentDate >= now)
                .Include(a => a.Patient) // Include the related Patient data
                .OrderBy(a => a.AppointmentDate) // Order by the appointment date (ascending)
                .Take(10) // Take the top 10 closest appointments
                .ToList();

            if (appointments.Count > 0)
            {
                var upcomingAppointments = appointments.Select(a => new
                {
                    a.AppointmentId,
                    a.Status,
                    a.AppointmentDate,
                    PatientFullName = a.Patient.FullName,
                }).ToList();

                return Json(new
                {
                    success = true,
                    upcomingAppointments = upcomingAppointments
                });
            }

            return Json(new
            {
                success = false
            });
        }
    }
}