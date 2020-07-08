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
            var dataStorageMock = MockDataStorageFactory.GetMock();
            var dataStorage = dataStorageMock.Object;

            IAppointmentService appointmentService = new AppointmentService(dataStorage);

            Doctor expectedDoc = dataStorage.GetDoctors().First(d => d.Name == "B");
            Patient expectedPat = dataStorage.GetPatients().First(p => p.Name == "1");
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
            var dataStorageMock = MockDataStorageFactory.GetMock();
            var dataStorage = dataStorageMock.Object;

            var appointmentService = new AppointmentService(dataStorage);

            var foundDoctors = appointmentService.FindMoreFreeDoctors().ToList();
            Assert.AreEqual(foundDoctors.Count(), 2);
            Assert.AreEqual(foundDoctors[0].Name, "B");
            Assert.AreEqual(foundDoctors[1].Name, "A");
        }
    }
}
