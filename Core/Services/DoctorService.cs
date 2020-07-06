using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class DoctorService : EmployeeService
    {
        public DoctorService(IDataAccessService dataAccessService)
        : base(dataAccessService)
        {
        }

        public IEnumerable<Doctor> SortDoctorsByBusyness()
        {
            return DataAccessService.GetAppointments()
                .GroupBy(appointment => appointment.Doctor)
                .Select(grouping => new
                {
                    Doctor = grouping.Key,
                    Length = grouping.Sum(app => (app.End - app.Start).Ticks)
                }).OrderBy(app => app.Length)
                .Select(app => app.Doctor);
        }
    }
}
