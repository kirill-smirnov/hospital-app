using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDataStorage DataStorage;
        private readonly IDataUtilsService DataUtilsService;
        public DoctorController(IDataStorage dataStorage, IDataUtilsService dataUtilsService)
        {
            DataStorage = dataStorage;
            DataUtilsService = dataUtilsService;
        }

        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return DataStorage.GetDoctors();
        }

        [HttpGet("{id}")]
        public Doctor Get(string id)
        {
            return DataUtilsService.GetDoctor(id);
        }

        [HttpPost]
        public void Post(Doctor doctor)
        {
            DataStorage.CreateDoctor(doctor);
        }

        [HttpPut("{id}")]
        public void Put(string id, Doctor doctor)
        {
            DataStorage.UpdateDoctor(doctor);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var doctor = DataUtilsService.GetDoctor(id);
            DataStorage.DeleteDoctor(doctor);
        }
    }
}
