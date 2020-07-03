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
        public void TestAppointmentCreation()
        {
            IDataAccessService data = new OfflineDataAccessService();
            IAppointmentService appointmentService = new AppointmentService(data);
            
            Doctor doc = data.GetDoctors().ElementAt(1);
            IEnumerable<Appointment> list = appointmentService.GetAppointments(doc);

            foreach (var app in list)
                Assert.AreEqual(app.Doctor, doc);
        }
    }
}
