using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IDataStorage {
        IQueryable<Patient> GetPatients();
        Patient GetPatient(string id);
        void CreatePatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
        IQueryable<Doctor> GetDoctors();
        Doctor GetDoctor(string id);
        void CreateDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
        IQueryable<Appointment> GetAppointments();
        Appointment GetAppointment(string id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
