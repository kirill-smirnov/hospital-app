using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace App.Controllers
{
    [Authorize]
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IDataStorage DataStorage;
        private readonly IDataUtilsService DataUtilsService;

        public PatientController(IDataStorage dataStorage, IDataUtilsService dataUtilsService)
        {
            DataStorage = dataStorage;
            DataUtilsService = dataUtilsService;
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return DataStorage.GetPatients();
        }

        [HttpGet("{id}")]
        public Patient Get(string id)
        {
            return DataUtilsService.GetPatient(id);
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
            var patient = DataUtilsService.GetPatient(id);
            DataStorage.DeletePatient(patient);
        }
    }
}
