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
            IAppointmentService ds = new AppointmentService(data.Object);

            data.Setup(a => a.GetDoctors()).Returns((IQueryable<Doctor>)ds.FindMoreFreeDoctors());

            Doctor doctor = ds.FindMoreFreeDoctors().ElementAt(0);
            Doctor expected = data.Object.GetDoctors().ElementAt(0);
            Assert.AreEqual(expected, doctor);
        }
    }
}
