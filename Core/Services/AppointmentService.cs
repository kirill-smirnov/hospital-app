using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        protected IDataAccessService DataAccessService { get; }
        public AppointmentService(IDataAccessService service)
        {
            DataAccessService = service;
        }        
        IEnumerable<Appointment> IAppointmentService.GetAppointments(Doctor doctor)
        {
            return DataAccessService.GetAppointments().Where(Appointment => Appointment.Doctor == doctor);
        }

        public IEnumerable<Doctor> FindMoreFreeDoctors()
        {
            return DataAccessService.GetAppointments()
                .GroupBy(appointment => appointment.Doctor)
                .Select(grouping => new
                {
                    Doctor = grouping.Key,
                    Length = grouping.Sum(app => (app.End - app.Start).Ticks)
                }).OrderBy(app => app.Length)
                .Select(app => app.Doctor);
        }
    }
}
