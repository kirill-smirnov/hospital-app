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
    public class DataStorageTest
    {
        [TestMethod]
        public void UpdateAppointmentTest()
        {
            var dataStorage = new OfflineDataStorage();
            var doctor = new Doctor { Name = "A" };
            var appToUpdate = dataStorage.GetAppointments().ElementAt(0);

            appToUpdate.Doctor = doctor;
            dataStorage.UpdateAppointment(appToUpdate);

            var appAfterUpdate = dataStorage.GetAppointment(appToUpdate.Id);
            Assert.AreEqual(appAfterUpdate.Doctor.Name, doctor.Name);
        }

        [TestMethod]
        public void DeleteAppointmentTest()
        {
            var dataStorage = new OfflineDataStorage();
            int appointmentCount = dataStorage.GetAppointments().Count();
            var appToDelete = dataStorage.GetAppointments().ElementAt(0);

            dataStorage.DeleteAppointment(appToDelete);

            var appointmentsAfterDelete = dataStorage.GetAppointments();
            Assert.AreEqual(appointmentCount - 1, appointmentsAfterDelete.Count());
            CollectionAssert.DoesNotContain(appointmentsAfterDelete.ToList(), appToDelete);

        }
    }
}