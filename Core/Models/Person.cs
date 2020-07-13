using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public enum Role { Patient, ReceptionStaff, Doctor }
    public class Person
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Id == person.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
