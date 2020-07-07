using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IDataAccessService DataAccessService;

        public AppointmentController(IDataAccessService dataAccessService)
        {
            DataAccessService = dataAccessService;
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return DataAccessService.GetAppointments().AsEnumerable();
        }
    }
}
