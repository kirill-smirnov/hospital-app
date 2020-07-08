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
        public void AppointmentGettersTest()
        {
            var dataAccessServiceMock = MockDataAccessFactory.GetMock();
            var dataAccessService = dataAccessServiceMock.Object;

            IAppointmentService appointmentService = new AppointmentService(dataAccessService);

            Doctor expectedDoc = dataAccessService.GetDoctors().First(d => d.Name == "B");
            Patient expectedPat = dataAccessService.GetPatients().First(p => p.Name == "1");
            IEnumerable<Appointment> docList = appointmentService.GetAppointments(expectedDoc);
            IEnumerable<Appointment> patList = appointmentService.GetAppointments(expectedPat);

            Assert.AreEqual(1, docList.Count());
            foreach (var app in docList)
            {
                Assert.AreEqual("B", app.Doctor.Name);
            }

            Assert.AreEqual(2, patList.Count());
            foreach (var app in patList)
            {
                Assert.AreEqual("1", app.Patient.Name);
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
