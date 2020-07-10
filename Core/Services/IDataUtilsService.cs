using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IDataUtilsService
    {
        IEnumerable<Appointment> GetAppointments(Doctor doctor);
        IEnumerable<Appointment> GetAppointments(Patient patient);
        IEnumerable<Doctor> FindMoreFreeDoctors();
        Patient GetPatient(string id);
        Doctor GetDoctor(string id);
        IEnumerable<object> GetClientAppointments(FilterOptions options);
    }
}
