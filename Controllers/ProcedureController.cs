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
    public class ProcedureController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public ProcedureController()
        {
            _Context = new ApplicationDbContext();
        }

        /// <summary>
        /// Getting all procedures
        /// </summary>
        /// <returns>A lsit of procedures with procedure full name</returns>
        /// 
        [HttpGet]
        public ActionResult GetAllProcedures()
        {
            var proceduresData = _Context.Procedures
                .ToList();

            if (proceduresData.Count > 0)
            {
                var procedures = proceduresData.Select(a => new
                {
                    a.ProcedureId,
                    a.ProcedureName,
                }).ToList();

                return Json(new
                {
                    success = true,
                    proceduresCount = procedures.Count,
                    procedures = procedures,
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}