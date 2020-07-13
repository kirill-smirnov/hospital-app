using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class FilterOptions
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
    }

    public class DataUtilsService : IDataUtilsService
    {
        protected IDataStorage DataStorage { get; }
        public DataUtilsService(IDataStorage service)
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

        public IEnumerable<object> GetClientAppointments(FilterOptions options)
        {
            IEnumerable<Appointment> query = DataStorage.GetAppointments();

            if (!string.IsNullOrEmpty(options.DoctorId))
            {
                var doctor = GetDoctor(options.DoctorId);
                query = query.Where(appt => appt.Doctor == doctor);
            }

            else if (!string.IsNullOrEmpty(options.PatientId))
            {
                var patient = GetPatient(options.PatientId);
                query = query.Where(appt => appt.Patient == patient);
            }

            else
                query = DataStorage.GetAppointments();

            return query.Select(a => {
                var doctor = GetDoctor(a.Doctor.Id);
                var patient = GetPatient(a.Patient.Id);

                return new
                {
                    id = a.Id,
                    patient = new
                    {
                        id = patient.Id,
                        name = patient.Name
                    },
                    doctor = new
                    {
                        id = doctor.Id,
                        name = doctor.Name
                    },
                    start = a.Start,
                    end = a.End,
                    commentary = a.Commentary
                };
            });
        }

        public Person GetPatient(string username, string password)
        {
            return DataStorage.GetPatients()
                .FirstOrDefault(p => p.Username == username && p.Password == password);
        }

        public Person GetEmployee(string username, string password)
        {
            return DataStorage.GetDoctors()
                .FirstOrDefault(d => d.Username == username && d.Password == password);
        }
    }
}
