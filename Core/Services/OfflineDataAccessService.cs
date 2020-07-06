using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class OfflineDataAccessService : IDataAccessService
    {
        IQueryable<Patient> Patients;
        IQueryable<Doctor> Doctors;
        IQueryable<Appointment> Appointments;

        public OfflineDataAccessService()
        {
            Patients = (new List<Patient>
            {
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dsad geer", Height = 170, Weight = 67.6f, PassportInfo = "rehtrg", InsuranceInfo = "5435"},
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dhdf sdsfs", Height = 150, Weight = 47.6f, PassportInfo = "rg", InsuranceInfo = "5ytrydf5"},
            }).AsQueryable();

            Doctors = (new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "sadad as asd"},
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "s sda aasdq"}
            }).AsQueryable();

            Appointments = (new List<Appointment>
            {
                new Appointment {Patient = Patients.ElementAt(0), Doctor = Doctors.ElementAt(0), 
                    Time = DateTime.Now.AddDays(1), Length = new TimeSpan(0, 15, 0)},
                new Appointment {Patient = Patients.ElementAt(0), Doctor = Doctors.ElementAt(1),
                    Time = DateTime.Now.AddDays(2), Length = new TimeSpan(0, 20, 0)},
                new Appointment {Patient = Patients.ElementAt(1), Doctor = Doctors.ElementAt(1), 
                    Time = DateTime.Now.AddDays(3), Length = new TimeSpan(0, 15, 0)}
            }).AsQueryable();
        }

        public IQueryable<Appointment> GetAppointments()
        {
            return Appointments;
        }
        public IQueryable<Doctor> GetDoctors()
        {
            return Doctors;
        }

        public IQueryable<Patient> GetPatients()
        {
            return Patients;

        }
    }
}
