using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    interface IEmployeeService
    {
    }

    public class EmployeeService : IEmployeeService
    {
        protected IDataAccessService DataAccessService;

        public EmployeeService(IDataAccessService dataAccessService)
        {
            DataAccessService = dataAccessService;
        }
    }
}
