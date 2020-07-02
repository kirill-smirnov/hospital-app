using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    interface ICreationManager
    {
        List<Appoinment> GetAppoinments(Patient patient, Doctor doctor);
    }
    public class CreationManager : ICreationManager
    {
        private List<Patient> patients;
        private List<Doctor> doctors;
        private List<Appoinment> appoinments;
        public CreationManager()
        {
            
        }

        public void CreatePatients()
        {
            patients = new List<Patient>
            {
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dsad geer", Height = 170, Weight = 67.6f, PassportInfo = "rehtrg", InsuranceInfo = "5435"},
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dhdf sdsfs", Height = 150, Weight = 47.6f, PassportInfo = "rg", InsuranceInfo = "5ytrydf5"},
            };
        }

        public void CreateDoctors()
        {
            doctors = new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "sadad as asd"},
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "s sda aasdq"}
            };
        }

        public void CreateAppoinments()
        {
            appoinments = new List<Appoinment>
            {
                new Appoinment {Patient = patients[0], Doctor = doctors[0], Time = DateTime.Now.AddDays(1)},
                new Appoinment {Patient = patients[1], Doctor = doctors[1], Time = DateTime.Now.AddDays(3)}
            };
        }

        public void AttachPatientsAndDoctors()
        {
            doctors[0].AddPatient(patients[0]);
            doctors[1].AddPatient(patients[1]);
        }
        public List<Appoinment> GetAppoinments(Patient patient, Doctor doctor)
        {
            return appoinments.Where(appoinment => appoinment.Patient == patient && appoinment.Doctor == doctor).ToList();
        }
    }
}
