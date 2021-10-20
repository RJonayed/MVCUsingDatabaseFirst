using MVCModelAndAnnotationAndValidation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModelAndAnnotationAndValidation.Controllers
{
    public class HomeController : Controller
    {

        EmployeeRepository repo = new EmployeeRepository();
        public ActionResult Index()
        {
           ///throw new Exception("Unknown error occured");
            return View();
        }
    }
}