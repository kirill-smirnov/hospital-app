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
        private readonly IDataStorage DataStorage;
        private readonly IAppointmentService AppointmentService;

        public AppointmentController(IDataStorage dataStorage, IAppointmentService appointmentService)
        {
            DataStorage = dataStorage;
            AppointmentService = appointmentService;
        }

        [HttpPost]
        public ActionResult<Appointment> CreateAppointment(Appointment appointment)
        {
            var patient = appointment.Patient;
            patient.Appointments.Add(appointment);
            DataStorage.CreateAppointment(appointment);
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
                var doctor = DataStorage.GetDoctor(doctorId);
                query = AppointmentService.GetAppointments(doctor);
            }

            else if (!string.IsNullOrEmpty(patientId))
            {
                var patient = DataStorage.GetPatient(patientId);
                query = AppointmentService.GetAppointments(patient);
            }

            else
                query = DataStorage.GetAppointments();

            return query.Select(a => new { 
                id = a.Id, patientId = a.Patient.Id, doctorId = a.Doctor.Id,
                start = a.Start, end = a.End, commentary = a.Commentary
            });
        }

        [HttpGet("{id}")]
        public Appointment GetAppointment(string id)
        {
            return DataStorage.GetAppointment(id);
        }

        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment(string id, Appointment appointment)
        {
            DataStorage.UpdateAppointment(appointment);

            return Ok(new { Message = "Success" });
        }

        [HttpDelete("{id}")]
        public ActionResult<Appointment> DeleteAppointment(string id)
        {
            var appointment = DataStorage.GetAppointment(id);
            DataStorage.DeleteAppointment(appointment);

            return Ok(new { Message = "Success" });
        }
    }
}
