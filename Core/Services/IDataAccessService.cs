using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IDataAccessService {
        IQueryable<Patient> GetPatients();
        IQueryable<Doctor> GetDoctors();
        IQueryable<Appointment> GetAppointments();
        Appointment GetAppointment(string id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
