using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public enum Position { ReceptionStaff, Nurse, Doctor }
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
       
    }

    public class Doctor: Employee
    {
        // Position = Position.Doctor
        public List<Patient> Patients { get; set; }
        public Schedule Schedule { get; set; }

        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
            patient.Doctor = this;
        }
    }

    public class Schedule 
    { }
}
