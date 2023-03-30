using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhsImsApp.Models;

namespace NhsImsApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid request");
            //}

            //var user = _context.Staffs.FirstOrDefault(u => u.Username == model.Username);

            //if (user == null || !VerifyPassword(model.Password, user.Password))
            //{
            //    return BadRequest(new { success = false, error = "Invalid username or password." });
            //}

            // Login successful
            return Json(new { success = true, message = "Login successful" });
        }
    }
    
}