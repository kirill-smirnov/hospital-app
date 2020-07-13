using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public enum Position { ReceptionStaff, Nurse, Doctor }
    public class Employee: Person
    {
         public string Name { get; set; }

        public Position Position { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   Id == employee.Id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }

    public class Doctor: Employee
    {
        public Doctor() : base()
        {
            Position = Position.Doctor;
        }
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public Schedule Schedule { get; set; }

        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
            patient.Doctor = this;
        }

        public override bool Equals(object obj)
        {
            return obj is Doctor doctor &&
                   Id == doctor.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }

    public class Schedule 
    { }
}
