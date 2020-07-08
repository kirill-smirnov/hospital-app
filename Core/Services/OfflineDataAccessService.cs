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
                new Appointment {Id = Guid.NewGuid().ToString(), Patient = Patients.ElementAt(0), 
                    Doctor = Doctors.ElementAt(0), Start = DateTime.Now.AddDays(1), 
                    End = DateTime.Now.AddDays(1).AddMinutes(15)},
                new Appointment {Id = Guid.NewGuid().ToString(), Patient = Patients.ElementAt(0),
                    Doctor = Doctors.ElementAt(1), Start = DateTime.Now.AddDays(2), 
                    End = DateTime.Now.AddDays(2).AddMinutes(20)},
                new Appointment {Id = Guid.NewGuid().ToString(), Patient = Patients.ElementAt(1), 
                    Doctor = Doctors.ElementAt(1), Start = DateTime.Now.AddDays(3), 
                    End = DateTime.Now.AddDays(3).AddMinutes(15)}
            }).AsQueryable();
        }
        public IQueryable<Appointment> GetAppointments()
        {
            return Appointments;
        }

        public Appointment GetAppointment(string id)
        {
            return Appointments.FirstOrDefault(app => app.Id == id);
        }

        public IQueryable<Doctor> GetDoctors()
        {
            return Doctors;
        }

        public Doctor GetDoctor(string id)
        {
            return Doctors.FirstOrDefault(d => d.Id == id);
        }

        public IQueryable<Patient> GetPatients()
        {
            return Patients;
        }

        public Patient GetPatient(string id)
        {
            return Patients.FirstOrDefault(p => p.Id == id);
        }

        public void CreateAppointment(Appointment appointment)
        {
            Appointments = Appointments.Concat(new List<Appointment> { appointment });
        }

        public void UpdateAppointment(Appointment appointment)
        {
            var appToUpdate = GetAppointment(appointment.Id);

            if (appToUpdate == null)
                return;

            Appointments = Appointments.Select(a => a == appToUpdate ? appointment : a);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            Appointments = Appointments.Where(app => app != appointment);
        }
    }
}
