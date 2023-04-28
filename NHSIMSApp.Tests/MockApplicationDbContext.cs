using System.Data.Entity;
using NhsImsApp.Models;
using NhsImsApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace NHSIMSApp.Tests
{
    public class MockApplicationDbContext : ApplicationDbContext
    {
        public MockApplicationDbContext() : base()
        {
            this.Staffs = new DbSetMock<Staff>();
            this.Appointments = GetMockDbSetForAppointments();
            this.Patients = GetMockDbSetForPatients();
        }

        public new DbSetMock<Staff> Staffs { get; set; }
        public new DbSet<Appointment> Appointments { get; set; }
        public new DbSet<Patient> Patients { get; set; }

        private static DbSet<Appointment> GetMockDbSetForAppointments()
        {
            var data = new List<Appointment>
            {
                // Add mock appointments here
            }.AsQueryable();

            var mockDbSet = new Moq.Mock<DbSet<Appointment>>();
            mockDbSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet.Object;
        }

        private static DbSet<Patient> GetMockDbSetForPatients()
        {
            var data = new List<Patient>
            {
                // Add mock patients here
            }.AsQueryable();

            var mockDbSet = new Moq.Mock<DbSet<Patient>>();
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet.Object;
        }

    }

    public class DbSetMock<T> : DbSet<T>, IDbSet<T> where T : class
    {
        public override T Add(T entity)
        {
            return base.Add(entity);
        }

    }
}