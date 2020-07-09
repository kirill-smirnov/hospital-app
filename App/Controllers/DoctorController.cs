using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDataStorage DataStorage;
        public DoctorController(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return DataStorage.GetDoctors();
        }

        [HttpGet("{id}")]
        public Doctor Get(string id)
        {
            return DataStorage.GetDoctor(id);
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
            var doctor = DataStorage.GetDoctor(id);
            DataStorage.DeleteDoctor(doctor);
        }
    }
}
