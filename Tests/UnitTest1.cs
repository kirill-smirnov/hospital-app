using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Core.Models;
using System;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAppoinments()
        {
            CreationManager cm = new CreationManager();

            cm.CreatePatients();
            cm.CreateDoctors();
            cm.AttachPatientsAndDoctors();
            cm.CreateAppoinments();
        }
    }
}
