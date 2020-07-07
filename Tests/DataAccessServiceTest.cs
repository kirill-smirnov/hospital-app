using Core.Models;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests 
{

    [TestClass]
    public class DataAccessServiceTest
    {
        [TestMethod]
        public void UpdateAppointmentTest()
        {
            var dataAccessService = new OfflineDataAccessService();
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
            int appointmentCount = dataAccessService.GetAppointments().Count();
            var appToDelete = dataAccessService.GetAppointments().ElementAt(0);

            dataAccessService.DeleteAppointment(appToDelete);

            var appointmentsAfterDelete = dataAccessService.GetAppointments();
            Assert.AreEqual(appointmentCount - 1, appointmentsAfterDelete.Count());
            CollectionAssert.DoesNotContain(appointmentsAfterDelete.ToList(), appToDelete);

        }
    }
}