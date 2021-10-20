using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModelAndAnnotationAndValidation.Repositories
{
    public class EmployeeMataData
    {
        [Remote("IsEmployeeNameAvailable","Employees",ErrorMessage ="Employee Name already is in use")]
        public string EmployeeName { get; set; }
    }
}