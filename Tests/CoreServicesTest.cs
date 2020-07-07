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


        [TestMethod]
        public void UpdateAppointmentTest()
        {
            var dataAccessService = new OfflineDataAccessService();
            var appointmentService = new AppointmentService(dataAccessService);
            var doctor = new Doctor { Name = "A" };
            var appToUpdate = dataAccessService.GetAppointments().ElementAt(0);

            appToUpdate.Doctor = doctor;
            appointmentService.UpdateAppointment(appToUpdate);

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

            appointmentService.DeleteAppointment(appToDelete);

            var appointmentsAfterDelete = dataAccessService.GetAppointments();
            Assert.AreEqual(appointmentCount-1, appointmentsAfterDelete.Count());
            CollectionAssert.DoesNotContain(appointmentsAfterDelete.ToList(), appToDelete);
        }
    }
}
