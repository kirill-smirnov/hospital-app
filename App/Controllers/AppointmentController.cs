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

        [HttpPost]
        public ActionResult<Appointment> CreateAppointment(Appointment appointment)
        {
            var patient = appointment.Patient;
            patient.Appointments.Add(appointment);
            DataAccessService.CreateAppointment(appointment);
            return Ok(new { Message = "Success"});
        }

        [HttpGet]
        public IEnumerable<object> GetAppointments()
        {
            IEnumerable<Appointment> query;

            string patientId = HttpContext.Request.Query["patientId"].ToString();
            string doctorId = HttpContext.Request.Query["doctorId"].ToString();

            if (!string.IsNullOrEmpty(doctorId))
            {
                var doctor = DataAccessService.GetDoctors().FirstOrDefault(d => d.Id == doctorId);
                query = AppointmentService.GetAppointments(doctor);
            }

            else if (!string.IsNullOrEmpty(patientId))
            {
                var patient = DataAccessService.GetPatients().FirstOrDefault(p => p.Id == patientId);
                query = AppointmentService.GetAppointments(patient);
            }

            else
                query = DataAccessService.GetAppointments();

            return query.Select(a => new { 
                id = a.Id, patientId = a.Patient.Id, doctorId = a.Doctor.Id,
                start = a.Start, end = a.End, commentary = a.Commentary
            });
        }

        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment(string id, Appointment appointment)
        {
            DataAccessService.UpdateAppointment(appointment);

            return Ok(new { Message = "Success" });
        }

        [HttpDelete("{id}")]
        public ActionResult<Appointment> DeleteAppointment(string id)
        {
            var appointment = DataAccessService.GetAppointment(id);
            DataAccessService.DeleteAppointment(appointment);

            return Ok(new { Message = "Success" });
        }
    }
}
