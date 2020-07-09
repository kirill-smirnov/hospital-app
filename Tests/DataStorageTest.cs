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
        public void CreateAppointmentTest()
        {
            var dataStorage = new OfflineDataStorage();
            var count = dataStorage.GetAppointments().Count();
            var patient = dataStorage.GetPatients().First();
            var doctor = dataStorage.GetDoctors().First();

            var time = DateTime.Now.AddHours(1);
            var appointment = new Appointment
            {
                Patient = patient,
                Doctor = doctor,
                Start = time,
                End = time.AddMinutes(15)
            };

            dataStorage.CreateAppointment(appointment);

            var appointmentsAfterCreate = dataStorage.GetAppointments().ToList();
            Assert.AreEqual(count + 1, appointmentsAfterCreate.Count());
            CollectionAssert.Contains(appointmentsAfterCreate, appointment);
        }

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
        
        [TestMethod]
        public void CreatePatientTest()
        {
            var dataStorage = new OfflineDataStorage();
            int count = dataStorage.GetPatients().Count();
            var patient = new Patient { Name = "ABC", Weight = 50, Height = 150 };

            dataStorage.CreatePatient(patient);
            var patientsAfterCreate = dataStorage.GetPatients();

            Assert.AreEqual(count+1, patientsAfterCreate.Count());
            Assert.AreEqual(patient, patientsAfterCreate.FirstOrDefault(p => p.Id == patient.Id));
        }

        [TestMethod]
        public void UpdatePatientTest()
        {
            var dataStorage = new OfflineDataStorage();

            var patientToUpdate = dataStorage.GetPatients().Last();

            patientToUpdate.Name = "QWERTY";

            dataStorage.UpdatePatient(patientToUpdate);
            Assert.AreEqual("QWERTY", dataStorage.GetPatients().Last().Name);
        }

        [TestMethod]
        public void DeletePatientTest()
        {
            var dataStorage = new OfflineDataStorage();
            int count = dataStorage.GetPatients().Count();
            var patToDelete = dataStorage.GetPatients().First();

            dataStorage.DeletePatient(patToDelete);

            var patientsAfterDelete = dataStorage.GetPatients();
            Assert.AreEqual(count - 1, patientsAfterDelete.Count());
            CollectionAssert.DoesNotContain(patientsAfterDelete.ToList(), patToDelete);
        }
    }
}