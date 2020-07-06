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
                    Count = grouping.Count()
                }).OrderBy(app => app.Count)
                .Select(app => app.Doctor);
        }
    }
}
