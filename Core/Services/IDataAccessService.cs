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

    }
}
