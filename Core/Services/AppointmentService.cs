using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        protected IDataStorage DataStorage { get; }
        public AppointmentService(IDataStorage service)
        {
            DataStorage = service;
        }        
        public IEnumerable<Appointment> GetAppointments(Doctor doctor)
        {
            return DataStorage.GetAppointments().Where(appointment => appointment.Doctor == doctor);
        }
        public IEnumerable<Appointment> GetAppointments(Patient patient)
        {
            return DataStorage.GetAppointments().Where(appointment => appointment.Patient == patient);
        }
        public IEnumerable<Doctor> FindMoreFreeDoctors()
        {
            return DataStorage.GetAppointments()
                .GroupBy(appointment => appointment.Doctor)
                .Select(grouping => new
                {
                    Doctor = grouping.Key,
                    Length = grouping.Sum(app => (app.End - app.Start).Ticks)
                }).OrderBy(app => app.Length)
                .Select(app => app.Doctor);
        }

        public Patient GetPatient(string id)
        {
            return DataStorage.GetPatients().FirstOrDefault(p => p.Id == id);
        }

        public Doctor GetDoctor(string id)
        {
            return DataStorage.GetDoctors().FirstOrDefault(d => d.Id == id);
        }
    }
}
