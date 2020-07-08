using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IDataAccessService DataAccessService;
        private readonly IAppointmentService AppointmentService;

        public AppointmentController(IDataAccessService dataAccessService, IAppointmentService appointmentService)
        {
            DataAccessService = dataAccessService;
            AppointmentService = appointmentService;
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return DataAccessService.GetAppointments().AsEnumerable();
        }
        [HttpGet("patient")]
        public IEnumerable<Appointment> GetPatientAppts(string id)
        {
            var patient = DataAccessService.GetPatients().FirstOrDefault(p => p.Id == id);
            var appts = AppointmentService.GetAppointments(patient);
            return appts;
        }
        [HttpGet("doctor")]
        public IEnumerable<Appointment> GetDoctorAppts(string id)
        {
            var doctor = DataAccessService.GetDoctors().FirstOrDefault(d => d.Id == id);
            return AppointmentService.GetAppointments(doctor);
        }
    }
}
