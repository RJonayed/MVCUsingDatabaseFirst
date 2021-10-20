using MVCModelAndAnnotationAndValidation.Models;
using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCModelAndAnnotationAndValidation.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
       /// EmployeeDBEntities dbobj = new EmployeeDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult PostLogin(TblUser obj)
        {
            using (var dbobj = new EmployeeDBEntities1())
            {
                var count = dbobj.TblUsers.Where(u => u.UserName == obj.UserName && u.Password_ == obj.Password_).Count();
                if (count<=0)
                {
                    ViewBag.Message = "Invalid User";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(obj.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            //return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult PostSignUp(TblUser obj)
        {
            using (var dbobj=new EmployeeDBEntities1())
            {
                bool isExists = !dbobj.TblUsers.Any(u => u.UserName == obj.UserName);
                if (isExists)
                {
                    dbobj.TblUsers.Add(obj);
                    dbobj.SaveChanges();
                    int count = dbobj.TblUsers.Count();
                    //if (count==1)
                    //{
                    //    return RedirectToAction("CreateRole","SuperAdmin");
                    //}
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "User Name Exists");
                    return View();
                }
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }
    }
}