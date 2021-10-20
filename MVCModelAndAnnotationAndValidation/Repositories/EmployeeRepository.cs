using MVCModelAndAnnotationAndValidation.Models;
using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Repositories
{
    
    public class EmployeeRepository
    {
        EmployeeDBEntities1 dbobj = new EmployeeDBEntities1();
        public IEnumerable<EmployeeViewModel> GetAllEmployees()
        {
            List<EmployeeViewModel> Emplist = dbobj.Employees.Select(e => new EmployeeViewModel
            {
                EmoployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                JoinDate = e.JoinDate,
                Email = e.Email,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl,
                DesignationTitle = e.Designation.DesignationTitle,
                DesignationId = e.DesignationId,
                PageTitle = "Employee List"

            }).ToList();
            return Emplist;
        }
        public Employee GetEmployee(int id)
        {
            Employee obj = dbobj.Employees.SingleOrDefault(e => e.EmployeeId == id);
            return obj;
        }
        public void SaveEmployee(Employee obj)
        {
            dbobj.Employees.Add(obj);
            dbobj.SaveChanges();
        }
        public void UpdateEmployee(Employee Upobj)
        {
            dbobj.Entry(Upobj).State = EntityState.Modified;
            dbobj.SaveChanges();
        }
        public void DeleteEmployee(int id)
        {
            Employee delEmp = GetEmployee(id);
            dbobj.Employees.Remove(delEmp);
            dbobj.SaveChanges();
        }
        public List<Designation> GetDesignations()
        {
            List<Designation> list = dbobj.Designations.ToList();
            return list;
        }
    }
}