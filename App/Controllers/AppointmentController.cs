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
        private readonly IDataUtilsService DataUtilsService;

        public AppointmentController(IDataStorage dataStorage, IDataUtilsService dataUtilsService)
        {
            DataStorage = dataStorage;
            DataUtilsService = dataUtilsService;
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
        public IEnumerable<object> GetAppointments([FromQuery] FilterOptions options)
        {
            return DataUtilsService.GetClientAppointments(options);
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
