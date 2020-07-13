using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Patient: Person
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public string PassportInfo { get; set; }
        public string InsuranceInfo { get; set; }
        public List<DiseaseRecord> DiseaseRecords { get; set; }
        public List<Analysis> Analyses { get; set; }
        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Patient(): base()
        {
            Appointments = new List<Appointment>();
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   Id == patient.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }

    public class DiseaseRecord
    {
        public string Record { get; set; }
    }

    public class Analysis
    {
        public string Record { get; set; }
    }
}
