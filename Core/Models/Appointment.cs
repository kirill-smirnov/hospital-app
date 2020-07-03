using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Appointment
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Time { get; set; }
        public string Commentary { get; set; }
    }
}
