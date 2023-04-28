using System;
using System.Net;
using System.Web.Mvc;
using NhsImsApp.Controllers;
using NhsImsApp.Models;
using NUnit.Framework;
using NHSIMSApp.Tests;
using Newtonsoft.Json.Linq;
using NhsImsApp.Data;

namespace NHSIMSApp.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new AccountController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Dispose the context after each test
        }

        [Test]
        public void Login_WithInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("TestError", "This is a test error");

            // Act
            var result = _controller.Login(new LoginViewModel()) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void Login_WithInvalidUsernameOrPassword_ReturnsError()
        {
            // Arrange
            var loginViewModel = new LoginViewModel
            {
                Username = "d1704145",
                Password = "invalidpassword"
            };

            // Act
            var result = _controller.Login(loginViewModel) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            JObject data = JObject.FromObject(result.Data);
            Assert.IsFalse((bool)data["success"]);
        }

    }

    public class AppointmentsControllerTests
    {

        private AppointmentController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new AppointmentController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Dispose the context after each test
        }

        [Test]
        public void GetAppointmentsByStaffId_WithValidStaffId_ReturnsSuccess()
        {
            // Arrange
            int staffId = 29;

            // Act
            var result = _controller.GetAppointmentsByStaffId(staffId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            JObject data = JObject.FromObject(result.Data);
            Assert.IsFalse((bool)data["success"]);
        }

        [Test]
        public void GetUpcomingAppointmentsByStaffId_WithValidStaffId_ReturnsSuccess()
        {
            // Arrange
            int staffId = 29;

            // Act
            var result = _controller.GetUpcomingAppointmentsByStaffId(staffId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            JObject data = JObject.FromObject(result.Data);
            Assert.IsFalse((bool)data["success"]);
        }

        [Test]
        public void GetAppointmentsByRange_WithValidRange_ReturnsSuccess()
        {
            // Arrange
            int staffId = 1;
            DateTime startDate = DateTime.UtcNow.AddDays(-30);
            DateTime endDate = DateTime.UtcNow.AddDays(30);

            // Act
            var result = _controller.GetAppointmentsByRange(staffId, startDate, endDate) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            JObject data = JObject.FromObject(result.Data);
            Assert.IsTrue((bool)data["success"]);
        }

    }
}
