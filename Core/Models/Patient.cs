using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public string PassportInfo { get; set; }
        public string InsuranceInfo { get; set; }
        public List<DiseaseRecord> DiseaseRecords { get; set; }
        public List<Analysis> Analyses { get; set; }
        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }

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
