using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    interface IDataManager
    {
        public List<Appoinment> GetAppoinments(Patient patient, Doctor doctor);
    }

    public class DataManager : IDataManager
    {
        private DataService Service;
        public DataManager(DataService service)
        {
            Service = service;
        }

        public List<Appoinment> GetAppoinments(Patient patient, Doctor doctor)
        {
            return Service.Appoinments.Where(appoinment => appoinment.Patient == patient && appoinment.Doctor == doctor).ToList();
        }
    }
}
