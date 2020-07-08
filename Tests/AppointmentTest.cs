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
    public class AppointmentTest
    {
        [TestMethod]
        public void AppointmentCreationTest()
        {
            var dataAccessServiceMock = MockDataAccessFactory.GetMock();
            var dataAccessService = dataAccessServiceMock.Object;

            IAppointmentService appointmentService = new AppointmentService(dataAccessService);

            Doctor expectedDoc = dataAccessService.GetDoctors().First(d => d.Name == "B");
            IEnumerable<Appointment> list = appointmentService.GetAppointments(expectedDoc);

            Assert.AreEqual(1, list.Count());
            foreach (var app in list)
            {
                Assert.AreEqual("B", app.Doctor.Name);
            }
        }

        [TestMethod]
        public void DoctorSortByBusynessTest()
        {
            var dataAccessServiceMock = MockDataAccessFactory.GetMock();
            var dataAccessService = dataAccessServiceMock.Object;

            var appointmentService = new AppointmentService(dataAccessService);

            var foundDoctors = appointmentService.FindMoreFreeDoctors().ToList();
            Assert.AreEqual(foundDoctors.Count(), 2);
            Assert.AreEqual(foundDoctors[0].Name, "B");
            Assert.AreEqual(foundDoctors[1].Name, "A");
        }
    }
}
