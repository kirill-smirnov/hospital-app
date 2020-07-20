using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class OfflineDataStorage : IDataStorage
    {
        IQueryable<Patient> Patients;
        IQueryable<Doctor> Doctors;
        IQueryable<Appointment> Appointments;

        public OfflineDataStorage()
        {
            Patients = (new List<Patient>
            {
                new Patient { Name = "dsad geer", Height = 170, Weight = 67.6f, PassportInfo = "rehtrg", InsuranceInfo = "5435",
                    LoginInfo = new LoginInfo("P1", "123")},
                new Patient { Name = "dhdf sdsfs", Height = 150, Weight = 47.6f, PassportInfo = "rg", InsuranceInfo = "5ytrydf5",
                    LoginInfo = new LoginInfo("P2", "qwe")}
            }).AsQueryable();

            Doctors = (new List<Doctor>
            {
                new Doctor { Name = "sadad as asd", LoginInfo = new LoginInfo("D1", "123")},
                new Doctor { Name = "s sda aasdq", LoginInfo = new LoginInfo("D2", "asd")}
            }).AsQueryable();

            Appointments = (new List<Appointment>
            {
                new Appointment {
                    Patient = Patients.ElementAt(0), 
                    Doctor = Doctors.ElementAt(0), 
                    Start = DateTime.Now.AddDays(1), 
                    End = DateTime.Now.AddDays(1).AddMinutes(15)},
                new Appointment {
                    Patient = Patients.ElementAt(0),
                    Doctor = Doctors.ElementAt(1), Start = DateTime.Now.AddDays(2), 
                    End = DateTime.Now.AddDays(2).AddMinutes(20)},
                new Appointment {
                    Patient = Patients.ElementAt(1), 
                    Doctor = Doctors.ElementAt(1), Start = DateTime.Now.AddDays(3), 
                    End = DateTime.Now.AddDays(3).AddMinutes(15)}
            }).AsQueryable();
        }
 
        public IQueryable<Doctor> GetDoctors()
        {
            return Doctors;
        }

        public void CreateDoctor(Doctor doctor)
        {
            Doctors = Doctors.Concat(new List<Doctor> { doctor });
        }

        public void UpdateDoctor(Doctor doctor)
        {
            Doctors = Doctors.Select(d => d.Id == doctor.Id ? doctor : d);
        }

        public void DeleteDoctor(Doctor doctor)
        {
            Doctors = Doctors.Where(d => d != doctor);
        }

        public IQueryable<Patient> GetPatients()
        {
            return Patients;
        }

        public void CreatePatient(Patient patient)
        {
            Patients = Patients.Concat(new List<Patient> { patient });
        }

        public void UpdatePatient(Patient patient)
        {
            Patients = Patients.Select(p => p.Id == patient.Id ? patient : p);
        }

        public void DeletePatient(Patient patient)
        {
            Patients = Patients.Where(p => p != patient);
        }

        public IQueryable<Appointment> GetAppointments()
        {
            return Appointments;
        }

        public Appointment GetAppointment(string id)
        {
            return Appointments.FirstOrDefault(app => app.Id == id);
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

        public IQueryable<Person> GetPeople()
        {
            return Patients.Select(p => (Person)p)
                .Concat(Doctors.Select(d => (Person)d));
        }
    }
}
