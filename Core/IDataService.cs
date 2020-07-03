using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    interface IDataService {}

    public class DataService : IDataService
    {
        public List<Patient> Patients;
        public List<Doctor> Doctors;
        public List<Appoinment> Appoinments;
    }

    public class MockData : DataService
    {
        public void CreatePatients()
        {
            Patients = new List<Patient>
            {
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dsad geer", Height = 170, Weight = 67.6f, PassportInfo = "rehtrg", InsuranceInfo = "5435"},
                new Patient { Id = Guid.NewGuid().ToString(), Name = "dhdf sdsfs", Height = 150, Weight = 47.6f, PassportInfo = "rg", InsuranceInfo = "5ytrydf5"},
            };
        }

        public void CreateDoctors()
        {
            Doctors = new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "sadad as asd"},
                new Doctor { Id = Guid.NewGuid().ToString(), Name = "s sda aasdq"}
            };
        }

        public void CreateAppoinments()
        {
            Appoinments = new List<Appoinment>
            {
                new Appoinment {Patient = Patients[0], Doctor = Doctors[0], Time = DateTime.Now.AddDays(1)},
                new Appoinment {Patient = Patients[1], Doctor = Doctors[1], Time = DateTime.Now.AddDays(3)}
            };
        }

        public void AttachPatientsAndDoctors()
        {
            Doctors[0].AddPatient(Patients[0]);
            Doctors[1].AddPatient(Patients[1]);
        }
    }
}
