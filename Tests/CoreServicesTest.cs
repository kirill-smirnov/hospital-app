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
            var dataAccessServiceMock = MockDataAccessFactory.GetMock();
            var dataAccessService = dataAccessServiceMock.Object;

            IAppointmentService appointmentService = new AppointmentService(dataAccessService);

            Doctor expectedDoc = dataAccessService.GetDoctors().First(d => d.Name == "B");
            IEnumerable<Appointment> list = appointmentService.GetAppointments(expectedDoc);

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

        [TestMethod]
        public void UpdateAppointmentTest()
        {
            var dataAccessService = new OfflineDataAccessService();
            var appointmentService = new AppointmentService(dataAccessService);
            var doctor = new Doctor { Name = "A" };
            var appToUpdate = dataAccessService.GetAppointments().ElementAt(0);

            appToUpdate.Doctor = doctor;
            dataAccessService.UpdateAppointment(appToUpdate);

            var appAfterUpdate = dataAccessService.GetAppointment(appToUpdate.Id);
            Assert.AreEqual(appAfterUpdate.Doctor.Name, doctor.Name);
        }

        [TestMethod]
        public void DeleteAppointmentTest()
        {
            var dataAccessService = new OfflineDataAccessService();
            var appointmentService = new AppointmentService(dataAccessService);
            int appointmentCount = dataAccessService.GetAppointments().Count();
            var appToDelete = dataAccessService.GetAppointments().ElementAt(0);
            var appId = appToDelete.Id;

            dataAccessService.DeleteAppointment(appToDelete);

            var appointmentsAfterDelete = dataAccessService.GetAppointments();
            Assert.AreEqual(appointmentCount - 1, appointmentsAfterDelete.Count());
            CollectionAssert.DoesNotContain(appointmentsAfterDelete.ToList(), appToDelete);
        }
    }
}
