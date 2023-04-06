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
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public PatientController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        /// GET: Patients assigned to the given staffId
        /// </summary>
        /// <param name="StaffId"></param>
        /// <returns>Patients list</returns>
        [HttpPost]
        public ActionResult GetPatientsByStaffId(int StaffId)
        {
            var patientsData = _Context.StaffPatients
                .Where(a => a.StaffId == StaffId)
                .Include(a => a.Patient)
                .ToList();

            var patientsCount = patientsData.Count();

            if (patientsCount > 0)
            {
                var patients = patientsData.Select(a => new
                {
                    a.PatientId,
                    fullName = a.Patient.FullName,
                }).ToList();

                return Json(new
                {
                    success = true,
                    patientsCount = patientsCount,
                    patients = patients

                });
            }

            return Json(new { success = false });

            
            
        }
    }
}