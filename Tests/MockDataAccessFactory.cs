using Core;
using Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    class MockDataAccessFactory
    {

        public static Mock<IDataAccessService> GetMock()
        {
            var doctors = new List<Doctor> {
                new Doctor {Name = "A"},
                new Doctor {Name = "B"}
            };
            var appointments = new List<Appointment>{
                new Appointment { Doctor = doctors[0], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15)},
                new Appointment { Doctor =  doctors[1], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(20)},
                new Appointment { Doctor =  doctors[0], Start = DateTime.Now,
                    End = DateTime.Now.AddMinutes(15)},
            };

            var dataAccessService = new Mock<IDataAccessService>();
            dataAccessService.Setup(a => a.GetDoctors()).Returns(doctors.AsQueryable());
            dataAccessService.Setup(a => a.GetAppointments()).Returns(appointments.AsQueryable());

            return dataAccessService;
        }
    }
}
