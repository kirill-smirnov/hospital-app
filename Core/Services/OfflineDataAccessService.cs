using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class OfflineDataAccessService : IDataAccessService
    {
        public IQueryable<Appointment> GetAppointments()
        {
            IQueryable<Patient> patients = GetPatients();
            IQueryable<Doctor> doctors = GetDoctors();


            return (new List<Appointment>
            {
                new Appointment {Patient = patients.ElementAt(0), Doctor = doctors.ElementAt(0), Time = DateTime.Now.AddDays(1)},
                new Appointment {Patient = patients.ElementAt(1), Doctor = doctors.ElementAt(1), Time = DateTime.Now.AddDays(3)}
            }).AsQueryable();
        }
        public IQueryable<Doctor> GetDoctors()
        {
            return (new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "sadad as asd"},
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "s sda aasdq"}
            }).AsQueryable();
        }

        public IQueryable<Patient> GetPatients()
        {
            return (new List<Patient>
            {
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dsad geer", Height = 170, Weight = 67.6f, PassportInfo = "rehtrg", InsuranceInfo = "5435"},
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dhdf sdsfs", Height = 150, Weight = 47.6f, PassportInfo = "rg", InsuranceInfo = "5ytrydf5"},
            }).AsQueryable();

        }
    }
}
