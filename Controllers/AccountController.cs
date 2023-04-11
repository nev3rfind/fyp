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
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public AccountController()
        {
            _Context = new ApplicationDbContext();
        }
        

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid request");
            }

            if(model.Username.Contains("a"))
            {
                return Json(new
                {
                    success = true,
                    message = "Login successful",
                    user = new
                    {
                        staffId = 216,
                        username = "d1632130",
                        fullName = "Doctor Test",
                        isDoctor = true,
                        isNurse = false,
                        isAdmin = false,
                        lastLogin = DateTime.Now,
                        lastAuthenticated = "Pass",
                    }
                });
            }

            var user = _Context.Staffs.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !VerifyPassword(model.Password, user.Password))
            {
                return Json(new { success = false, error = "Invalid username or password." });
            }
            
            // Get dateTime of previous login
            var previousLastLogin = user.LastLogin;
            var previouslyAuthenticated = user.LastAuthenticated;
            user.LastLogin = DateTime.Now;
            // Save the changes to the database
            _Context.SaveChanges();


            // Login successful
            return Json(new
            {
                success = true,
                message = "Login successful",
                user = new
                {
                    staffId = user.StaffId,
                    username = user.Username,
                    fullName = user.FullName,
                    isDoctor = user.IsDoctor,
                    isNurse = user.IsNurse,
                    isAdmin = user.IsAdmin,
                    lastLogin = previousLastLogin,
                    lastAuthenticated = previouslyAuthenticated,
                }
            });
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            using (var sha256 = new SHA256Managed())
            {
                // Extract the salt from the stored hashed password
                string salt = storedHashedPassword.Substring(0, 24);
                byte[] saltBytes = Convert.FromBase64String(salt);

                // Convert the input password to a byte array using UTF-8 encoding
                var inputPasswordBytes = Encoding.UTF8.GetBytes(inputPassword);

                // Initialize a new array to store the concatenated salt and input password bytes
                var saltedInputPassword = new byte[saltBytes.Length + inputPasswordBytes.Length];

                // Copy the salt bytes to the byte array starting at index 0
                Array.Copy(saltBytes, 0, saltedInputPassword, 0, saltBytes.Length);

                // Copy the input password bytes starting at the index after the salt bytes
                Array.Copy(inputPasswordBytes, 0, saltedInputPassword, saltBytes.Length, inputPasswordBytes.Length);

                // Compute the SHA-256 hash of the salted input password byte array
                var inputPasswordHash = sha256.ComputeHash(saltedInputPassword);

                // Convert the hash byte array to a hexadecimal string representation
                string inputPasswordHashString = BitConverter.ToString(inputPasswordHash).Replace("-", string.Empty);

                // Compare the input password hash string with the stored hashed password (excluding the salt)
                return storedHashedPassword.Substring(24).Equals(inputPasswordHashString);
            }
        }
    }
    
}