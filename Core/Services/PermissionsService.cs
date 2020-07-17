using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class PermissionsService
    {

        public static bool AllowEditAppointment(Person user, Appointment appointment)
        {
            return user.Role switch
            {
                Role.Patient => user.Role == Role.Patient,
                Role.Doctor => user.Id == appointment.Doctor.Id,
                Role.ReceptionStaff => true,
                _ => false
            };
        }
    }
}
