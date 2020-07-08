using Core;
using Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class MockDataStorageFactory
    {
        public static Mock<IDataStorage> GetMock()
        {
            var doctors = new List<Doctor> {
                new Doctor {Name = "A"},
                new Doctor {Name = "B"}
            };
            var patients = new List<Patient>
            {
                new Patient { Name = "1"},
                new Patient { Name = "2"}
            };

            var appointments = new List<Appointment> {
                new Appointment { Doctor = doctors[0], Patient = patients[0], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15)},
                new Appointment { Doctor =  doctors[1], Patient= patients[1], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(20)},
                new Appointment { Doctor =  doctors[0], Patient = patients[0], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15)},
            };

            var dataStorage = new Mock<IDataStorage>();
            dataStorage.Setup(a => a.GetDoctors()).Returns(doctors.AsQueryable());
            dataStorage.Setup(a => a.GetPatients()).Returns(patients.AsQueryable());
            dataStorage.Setup(a => a.GetAppointments()).Returns(appointments.AsQueryable());

            return dataStorage;
        }
    }
}
