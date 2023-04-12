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

        /// <summary>
        /// Get all patients matching search term
        /// </summary>
        /// <param name="StaffId"></param>
        /// <param name="searchTerm"></param>
        /// <returns>Matchign patients list</returns>
        [HttpPost]
        public ActionResult GetPatientsBySearchTerm(int staffId, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(new { success = false });
            }

            var patientsData = _Context.Patients
                .Where(a => a.FullName.Contains(searchTerm))
                .ToList();

            if (patientsData.Count > 0)
            {
                var staffPatients = _Context.StaffPatients
                    .Where(a => a.StaffId == staffId)
                    .ToList();

                var patients = patientsData.Select(a => new
                {
                    a.PatientId,
                    fullName = a.FullName,
                    belongsToStaff = staffPatients.Any(sp => sp.PatientId == a.PatientId)
                }).ToList();

                return Json(new
                {
                    success = true,
                    patientsCount = patients.Count,
                    patients = patients
                });
            }

            return Json(new { success = false });
        }

        /// <summary>
        /// Get patient procedures by patient Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Patient procedures list</returns>
        [HttpPost]
        public ActionResult GetPatientProceduresByPatientId(int patientId)
        {
            var procedures = _Context.PatientProcedures
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Procedure)
                .ToList();

            var proceduresCount = procedures.Count();

            if (proceduresCount > 0)
            {
                var proceduresData = procedures.Select(a => new
                {
                    a.ProcedureId,
                    a.Description,
                    procedureName = a.Procedure.ProcedureName
                }).ToList();

                return Json(new
                {
                    success = true,
                    proceduresCount = proceduresCount,
                    proceduresData = proceduresData

                });
            }

            return Json(new { success = false });

        }

        /// <summary>
        /// Get patient details by Id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Patient details</returns>
        [HttpPost]
        public ActionResult GetPatientDetailsByPatientId(int patientId)
        {
            try
            {
                var patientDetails = _Context.Patients
                    .Where(a => a.PatientId == patientId)
                    .FirstOrDefault();

                if (patientDetails != null)
                {
                    return Json(new
                    {
                        success = true,
                        fullName = patientDetails.FullName,
                        dob = patientDetails.Dob,
                        gender = patientDetails.Gender,
                    });
                }

                return Json(new
                {
                    success = false,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [HttpPost]
        public ActionResult GetPatientMedicationList(int patientId)
        {
            var patientMedicationsData = _Context.PatientMedications
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Medication)
                .ToList();

            if(patientMedicationsData.Count > 0)
            {
                var patientMedications = patientMedicationsData.Select(a => new
                {
                    a.Id,
                    a.StartDate,
                    a.EndDate,
                    MedicationName = a.Medication.MedicationName,
                    RemainingDays = (a.EndDate - a.StartDate)?.TotalDays,
                    Procentage = (a.EndDate - a.StartDate)?.TotalDays / 28 * 100,
                }).ToList();

                return Json(new { success = true, patientMedications = patientMedications });
            }

            return Json(new { success = false });
        }
    }
}