using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IDataStorage {
        IQueryable<Patient> GetPatients();
        IQueryable<Doctor> GetDoctors();
        IQueryable<Appointment> GetAppointments();
        Patient GetPatient(string id);
        Doctor GetDoctor(string id);
        Appointment GetAppointment(string id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
