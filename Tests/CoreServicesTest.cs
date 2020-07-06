using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Core.Models;
using System;
using Core.Services;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace Tests
{
    [TestClass]
    public class CoreServicesTest
    {
        [TestMethod]
        public void AppointmentCreationTest()
        {
            IDataAccessService data = new OfflineDataAccessService();
            IAppointmentService appointmentService = new AppointmentService(data);

            Doctor doc = data.GetDoctors().ElementAt(1);
            IEnumerable<Appointment> list = appointmentService.GetAppointments(doc);

            foreach (var app in list)
                Assert.AreEqual(app.Doctor, doc);
        }

        [TestMethod]
        public void DoctorSortByBusynessTest()
        {
            var data = new Mock<IDataAccessService>();
            data.Setup(a => a.GetDoctors())
                .Returns((new List<Doctor> { 
                    new Doctor {Name = "A"},
                    new Doctor {Name = "B"}
                }).AsQueryable());

            data.Setup(a => a.GetAppointments())
                .Returns((new List<Appointment>
                {
                    new Appointment { Doctor = data.Object.GetDoctors().ElementAt(0), Start = DateTime.Now, 
                        End = DateTime.Now.AddMinutes(15)},
                    new Appointment { Doctor = data.Object.GetDoctors().ElementAt(1), Start = DateTime.Now,
                        End = DateTime.Now.AddMinutes(20)},
                    new Appointment { Doctor = data.Object.GetDoctors().ElementAt(0), Start = DateTime.Now,
                        End = DateTime.Now.AddMinutes(15)},
                }).AsQueryable());

            IAppointmentService ds = new AppointmentService(data.Object);

            Doctor doctor = ds.FindMoreFreeDoctors().ElementAt(0);
            Doctor expected = data.Object.GetDoctors().ElementAt(1);
            Assert.AreEqual(expected, doctor);
        }
    }
}
