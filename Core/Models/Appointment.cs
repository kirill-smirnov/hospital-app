using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Commentary { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Appointment appointment &&
                   Id == appointment.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
