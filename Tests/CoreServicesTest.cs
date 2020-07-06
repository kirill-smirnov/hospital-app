using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Core.Models;
using System;
using Core.Services;
using System.Linq;
using System.Collections.Generic;

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
                Assert.AreEqual(app.Doctor.GetHashCode(), doc.GetHashCode());
        }

        [TestMethod]
        public void DoctorSortByBusynessTest()
        {
            IDataAccessService data = new OfflineDataAccessService();
            DoctorService ds = new DoctorService(data);

            Doctor doctor = ds.SortDoctorsByBusyness().ElementAt(0);
            Doctor expected = data.GetDoctors().ElementAt(0);
            Assert.AreEqual(expected.GetHashCode(), doctor.GetHashCode());
        }
    }
}
