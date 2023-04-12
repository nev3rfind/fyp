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
    public class MedicationController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public MedicationController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        /// Getting all medication
        /// </summary>
        /// <returns>A lsit of medication with medication full name</returns>
        /// 
        [HttpGet]
        public ActionResult GetAllMedications()
        {
            var medicationsData = _Context.Medications
                .ToList();

            if (medicationsData.Count > 0)
            {
                var medications = medicationsData.Select(a => new
                {
                    a.MedicationId,
                    a.MedicationName,
                }).ToList();

                return Json(new
                {
                    success = true,
                    medicationsCount = medications.Count,
                    medications = medications,
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}