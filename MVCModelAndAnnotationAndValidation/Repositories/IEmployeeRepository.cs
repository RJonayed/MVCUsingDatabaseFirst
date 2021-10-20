using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCModelAndAnnotationAndValidation.Repositories
{
    interface IEmployeeRepository
    {
        IEnumerable<EmployeeViewModel> GetEmployees();
        void GetEmployeeCount();
        
    }
}
