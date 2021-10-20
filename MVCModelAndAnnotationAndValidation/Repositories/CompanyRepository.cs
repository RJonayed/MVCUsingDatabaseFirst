using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Repositories
{
    public class CompanyRepository
    {
        EmployeeDBEntities1 db = new EmployeeDBEntities1();
        public IEnumerable<CompanyView> GetAllCompanies()
        {
            List<CompanyView> list = db.CompanyViews.Select(c => new CompanyView
            {
                Id = c.Id,
                Company1 = c.Company1,
                Phone = c.Phone,
                Email = c.Email,
                EstDate = c.EstDate
            }).ToList();
            return list;
        }
        public Company GetCompany(int id)
        {
            Company obj = db.Companies.SingleOrDefault(c => c.Id == id);
            return obj;
        }
        public void SaveCompany(Company obj)
        {
            db.Companies.Add(obj);
            db.SaveChanges();
        }
        public void UpdateCompany(Company Upobj)
        {
            db.Entry(Upobj).State =EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteCompany(int id)
        {
            Company delCom = GetCompany(id);
            db.Companies.Remove(delCom);
            db.SaveChanges();
        }
    }
}