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

namespace NhsImsApp.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public PrescriptionController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        /// Getting prescription details by staffId
        /// </summary>
        /// <param name="StaffId"></param>
        /// <returns>Pending prescriptions</returns>
        [HttpPost]
        public ActionResult GetPatientsPrescriptionByStaffId(int StaffId)
        {
            var prescriptions = _Context.PatientMedications
                .Where(pm => pm.StaffId == StaffId)
                .ToList();

            if (prescriptions.Count > 0)
            {
                var totalPrescription = prescriptions.Count();
                var renewedPrescriptions = prescriptions
                    .Where(p => p.IsRenewed.HasValue && p.IsRenewed.Value)
                    .Count();
                double renewedProcentage = (double)renewedPrescriptions / totalPrescription * 100;
                renewedProcentage = Math.Round(renewedProcentage, 0);

                return Json(new 
                { 
                    success = true,
                    totalPrescriptions = totalPrescription,
                    renewedPrescriptions = renewedPrescriptions,
                    renewedProcentage = renewedProcentage

                });
            }
            return Json(new
            {
                success = false
            });
        }

        /// <summary>
        /// Getting active and expired prescription count
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns>Active and expired prescription count</returns>
        [HttpPost]
        public ActionResult GetPrescriptionSummary(int staffId)
        {
            var currentDate = DateTime.Now;

            var medicationSummary = _Context.PatientMedications
                .Where(pm => pm.StaffId == staffId)
                .ToList();

            if (medicationSummary.Any())
            {
                var prescriptionSummary = new
                {
                    ActivePrescriptions = medicationSummary.Count(pm => pm.StartDate <= currentDate && pm.EndDate >= currentDate),
                    ExpiredPrescriptions = medicationSummary.Count(pm => pm.EndDate < currentDate),
                };

                return Json(new
                {
                    prescriptionSummary = prescriptionSummary,
                    success = true
                });
            }

            return Json(new
            {
                prescriptionSummary = 0,
                success = true
            });
        }

    }
} 