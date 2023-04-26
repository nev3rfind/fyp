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
            DateTime currentDate = DateTime.Now;
            DateTime oneMonthFromNow = currentDate.AddMonths(1);

            var patientMedicationsData = _Context.PatientMedications
                .Where(a => a.PatientId == patientId && a.EndDate <= oneMonthFromNow)
                .Include(a => a.Medication)
                .ToList();

            if (patientMedicationsData.Count > 0)
            {
                var patientMedications = patientMedicationsData.Select(a => new
                {
                    a.Id,
                    a.StartDate,
                    a.EndDate,
                    MedicationName = a.Medication.MedicationName,
                    RemainingDays = DateTime.DaysInMonth(a.EndDate.Value.Year, a.EndDate.Value.Month) - Math.Round((a.EndDate.Value - DateTime.Today).TotalDays),
                    Procentage = 100 - (Math.Round((a.EndDate.Value - DateTime.Today).TotalDays / DateTime.DaysInMonth(a.EndDate.Value.Year, a.EndDate.Value.Month) * 100)),
                    DaysInMonth = DateTime.DaysInMonth(a.EndDate.Value.Year, a.EndDate.Value.Month),
                }).ToList();

                return Json(new
                {
                    success = true, 
                    patientMedications = patientMedications,
                    medicationCount = patientMedicationsData.Count(),
                });
            }

            return Json(new { success = true, medicationCount = 0 });
        }

        /// <summary>
        /// Adds new patient medication records
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>True status if it was successfully added</returns>
        [HttpPost]
        public ActionResult AddPatientMedicationRecord(PatientMedicationInputModel inputModel)
        {
            // Create a new PatientMedication instance
            var newPatientMedication = new PatientMedication
            {
                PatientId = inputModel.PatientId,
                MedicationId = inputModel.MedicationId,
                StaffId = inputModel.StaffId,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                PrescriptionDate = DateTime.Now,
                IsRenewed = false,
            };

            // Add new record to the database
            _Context.PatientMedications.Add(newPatientMedication);

            _Context.SaveChanges();

            return Json(new { success = true });
        }

        /// <summary>
        /// Adds new patient procedure records
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>True status if it was successfully added</returns>
        [HttpPost]
        public ActionResult AddPatientProcedureRecord(PatientProcedureInputModel inputModel)
        {
            // Create a new PatientProcedure instance
            var newPatientProcedure = new PatientProcedure
            {
                PatientId = inputModel.PatientId,
                ProcedureId = inputModel.ProcedureId,
                CreatedDate = DateTime.Now,
                ActionDate = inputModel.ProcedureDate,
                Description = inputModel.ProcedureDescription,
                Status = "Completed",
            };

            // Add new record to the database
            _Context.PatientProcedures.Add(newPatientProcedure);

            _Context.SaveChanges();

            return Json(new { success = true });
        }

        /// <summary>
        /// Get patient procedures history
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Procedure list and count</returns>
        [HttpPost]
        public ActionResult GetPatientProcedureList(int patientId)
        {

            var patientProceduresData = _Context.PatientProcedures 
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Procedure)
                .ToList();

            if (patientProceduresData.Count > 0)
            {
                var patientProcedures = patientProceduresData.Select(a => new
                {
                    a.Id,
                    a.ActionDate,
                    a.Status,
                    ProcedureName = a.Procedure.ProcedureName,
                }).ToList();

                return Json(new
                {
                    success = true,
                    patientProcedures = patientProcedures,
                    procedureCount = patientProceduresData.Count(),
                });
            }

            return Json(new { success = true, procedureCount = 0 });
        }

        /// <summary>
        /// Gets a list of examinations
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Examinations list</returns>
        [HttpPost]
        public ActionResult GetPatientExaminationList(int patientId)
        {

            var patientExaminationsData = _Context.PatientExaminations
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Examination)
                .ToList();

            if (patientExaminationsData.Count > 0)
            {
                var patientExaminations = patientExaminationsData.Select(a => new
                {
                    a.Id,
                    a.ExaminationDate,
                    a.Analysis,
                    ExaminationType = a.Examination.ExaminationType,
                }).ToList();

                return Json(new
                {
                    success = true,
                    patientExamination = patientExaminations,
                    examinationCount = patientExaminationsData.Count(),
                });
            }

            return Json(new { success = true, examinationCount = 0 });
        }

        /// <summary>
        /// Get the closest appointment
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="patientId"></param>
        /// <returns>Closest appointments details</returns>
        [HttpPost]
        public ActionResult GetUpcomingAppointmentByPatientId(int staffId, int patientId)
        {
            DateTime currentDate = DateTime.Now;

            var closestAppointment = _Context.Appointments
                .Where(a => a.PatientId == patientId && a.StaffId == staffId && a.AppointmentDate > currentDate)
                .Include(a => a.Patient)
                .Include(a => a.Staff)
                .OrderBy(a => a.AppointmentDate)
                .FirstOrDefault();

            if (closestAppointment != null)
            {
                var appointmentData = new
                {
                    closestAppointment.AppointmentId,
                    AppointmentDate = closestAppointment.AppointmentDate,
                    Status = closestAppointment.Status,
                    AttendanceConfirmed = closestAppointment.AttendanceConfirmed,
                };

                return Json(new { success = true, appointment = appointmentData, appointmentStatus = 1 });
            }

            return Json(new { succeess = true, appointmentStatus = 0 });
        }

    }
}