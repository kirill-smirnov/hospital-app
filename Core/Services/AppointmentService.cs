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
    }
}
