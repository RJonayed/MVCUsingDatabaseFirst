using MVCModelAndAnnotationAndValidation.Models;
using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModelAndAnnotationAndValidation.Controllers
{
    public class DesignationController : Controller
    {
        // GET: Designation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateDesignationAjax()
        {
            return View();
        }
        public ActionResult AjaxFormExample(Designation obj)
        {
            EmployeeDBEntities1 dbobj = new EmployeeDBEntities1();
            dbobj.Designations.Add(obj);
            dbobj.SaveChanges();
            IEnumerable<Designation> list = dbobj.Designations.ToList();
            return View(list);
        }
    }
}