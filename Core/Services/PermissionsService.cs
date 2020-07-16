using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class PermissionsService
    {

        public static bool AllowUpdateAppintment(Person user, Appointment appointment)
        {
            if (user.Role == Role.Patient)
                return user.Id == appointment.Patient.Id;
            else if (user.Role == Role.ReceptionStaff)
                return true;
            else if (user.Role == Role.Doctor)
                return user.Id == appointment.Doctor.Id;
            else
                return false;
        }

        public static bool AllowDeleteAppointment(Person user, Appointment appointment)
        {
            if (user.Role == Role.Patient)
                return user.Id == appointment.Patient.Id;
            else if (user.Role == Role.ReceptionStaff)
                return true;
            else if (user.Role == Role.Doctor)
                return user.Id == appointment.Doctor.Id;
            else
                return false;
        }
    }
}
