using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IDataStorage DataStorage;

        public PatientController(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return DataStorage.GetPatients();
        }

        [HttpGet("{id}")]
        public Patient Get(string id)
        {
            return DataStorage.GetPatient(id);
        }


        [HttpPost]
        public void Post(Patient patient)
        {
            DataStorage.CreatePatient(patient);
        }

        [HttpPut("{id}")]
        public void Put(string id, Patient patient)
        {
            DataStorage.UpdatePatient(patient);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var patient = DataStorage.GetPatient(id);
            DataStorage.DeletePatient(patient);
        }
    }
}
