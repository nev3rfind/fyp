using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhsImsApp.Data;
using NhsImsApp.Models;
using Bogus;
using System.Security.Cryptography;
using System.Text;

namespace NhsImsApp.Seeders
{
    public static class StaffSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Staffs.Any())
            {
                var staffFaker = new Faker<Staff>()
                    .RuleFor(s => s.FullName, f => f.Name.FullName())
                    // One user at the same time can only be doctor, nurse or admin
                    .CustomInstantiator(f =>
                    {
                        bool isDoctor = f.Random.Bool();
                        bool isNurse = !isDoctor && f.Random.Bool();
                        return new Staff
                        {
                            IsDoctor = isDoctor,
                            IsNurse = isNurse,
                            IsAdmin = !isDoctor && !isNurse
                        };
                    })
                    .RuleFor(s => s.Username, (f, s) =>
                    {
                        if (s.IsDoctor)
                        {
                            return "d" + f.Random.Number(1000000, 9999999);
                        }
                        else if (s.IsNurse)
                        {
                            return "n" + f.Random.Number(1000000, 9999999);
                        }
                        else
                        {
                            return "a" + f.Random.Number(1000000, 9999999);
                        }
                    })
                    .RuleFor(s => s.Password, f => HashPassword("test123"))
                    .RuleFor(s => s.LastLogin, f => f.Date.Between(new DateTime(2022, 1, 1), DateTime.Now.AddYears(-1)))
                    .RuleFor(s => s.LastAuthenticated, f => f.PickRandom(new[] { "Pass", "MFA" }));

                var staffs = staffFaker.Generate(100);
                context.Staffs.AddRange(staffs);
                context.SaveChanges();
            }
        }

        // Generates hashed password with a unique salt for each staff member.
        private static string HashPassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                // Create a new instance to secure random number generator
                var rng = new RNGCryptoServiceProvider();
                // Intitialise array of 16 bites to store the salt
                var saltBytes = new byte[16];
                // Fills the array with cryptographically secure random bytes
                rng.GetBytes(saltBytes);
                // Converts the byte array to a Base64 string representation
                string salt = Convert.ToBase64String(saltBytes);
                // Converts the input string to a byte array using UTF-8 encoding
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                // Initialise new array which will store the concatenated salt and password bytes
                var saltedPassword = new byte[saltBytes.Length + passwordBytes.Length];
                // Copy the salt bytes to the byte array starting at index 0
                Array.Copy(saltBytes, 0, saltedPassword, 0, saltBytes.Length);
                // Copy the password bytes starting at the index after the salt bytes
                Array.Copy(passwordBytes, 0, saltedPassword, saltBytes.Length, passwordBytes.Length);
                // Computes the SHA-256 hash of the byte array and stores it in a new byte
                var hash = sha256.ComputeHash(saltedPassword);
                // Converts the hash byte array to a hexadecimal string representation using 
                // bit converter, remove any dashes and concateantes the salt string at the beginning
                // and returns the resulting string
                return salt + BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}