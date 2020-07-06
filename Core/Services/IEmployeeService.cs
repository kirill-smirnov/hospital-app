using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class EmployeeService
    {
        protected IDataAccessService DataAccessService;

        public EmployeeService(IDataAccessService dataAccessService)
        {
            DataAccessService = dataAccessService;
        }
    }
}
