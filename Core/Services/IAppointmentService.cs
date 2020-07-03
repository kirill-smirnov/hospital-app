﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAppointments(Doctor doctor);
    }
}