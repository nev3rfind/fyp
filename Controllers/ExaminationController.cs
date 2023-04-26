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
    public class ExaminationController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public ExaminationController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        /// Getting all examinations
        /// </summary>
        /// <returns>A list of examinations with examination type</returns>
        /// 
        [HttpGet]
        public ActionResult GetAllExaminations()
        {
            var examinationsData = _Context.Examinations
                .ToList();

            if (examinationsData.Count > 0)
            {
                var examinations = examinationsData.Select(a => new
                {
                    a.ExaminationId,
                    a.ExaminationType,
                }).ToList();

                return Json(new
                {
                    success = true,
                    examinationsCount = examinations.Count,
                    examinations = examinations,
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}