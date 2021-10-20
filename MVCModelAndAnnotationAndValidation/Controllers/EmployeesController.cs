using CrystalDecisions.CrystalReports.Engine;
using MVCModelAndAnnotationAndValidation.Models;
using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using MVCModelAndAnnotationAndValidation.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModelAndAnnotationAndValidation.Controllers
{
    public class EmployeesController : Controller
    {
        //IEmployeeRepository repo2;
       // private readonly IEmployeeRepository repo1;
        EmployeeRepository repo = new EmployeeRepository();
        //public EmployeesController(IEmployeeRepository service)
        //{
        //   repo1=  service;
        //}
        // GET: Employees

        public JsonResult IsEmployeeNameAvailable(string EmployeeName)
        {
            return Json(!repo.GetAllEmployees().Any(e => e.EmployeeName == EmployeeName), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration =10)]
        [Authorize(Roles ="Admin")]
        //[Authorize(Roles ="Viewers")]
        public ActionResult Index(string SearchString,string CurrentFilter,string SortOrder,int? page)
        {
            System.Threading.Thread.Sleep(3000);
            if (SearchString!=null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<EmployeeViewModel> list = repo.GetAllEmployees();
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.EmployeeName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.EmployeeName).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        [ChildActionOnly]
        [OutputCache(Duration = 10)]
       
        public string GetEmployeeCount()
        {
           return "Total Employee Count is :" + repo.GetAllEmployees().Count().ToString();
        }
        [HttpGet]
        public ViewResult Create()
        {
         List<Designation> list = repo.GetDesignations();
         ViewBag.Designations = list;
         return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            List<Designation> list = repo.GetDesignations();
            ViewBag.Designations = list;
            Employee emp = repo.GetEmployee(id);
            EmployeeViewModel obj = new EmployeeViewModel();
            obj.EmoployeeId = emp.EmployeeId;
            obj.EmployeeName = emp.EmployeeName;
            obj.Email = emp.Email;
            obj.ImageName = emp.ImageName;
            obj.JoinDate = emp.JoinDate;
            obj.ImageUrl = emp.ImageUrl;
            obj.DesignationId = emp.DesignationId;
            return View(obj);
        }
        [HttpPost]
        [ActionName("Create")]
        [Authorize(Roles ="Admin")]
        public ActionResult PostCreate(EmployeeViewModel obj)
        {
            var result = false;
            Employee obj1;
            if (obj.EmoployeeId==0)
            {
                if (ModelState.IsValid)
                {
                    obj1 = new Employee();
                    obj1.EmployeeName = obj.EmployeeName;
                    obj1.Email = obj.Email;
                    obj1.JoinDate = obj.JoinDate;
                    obj1.DesignationId = obj.DesignationId;
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj1.ImageName = fileName + extension;
                    obj1.ImageUrl = "~/Images" + obj1.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    repo.SaveEmployee(obj1);
                    result = true;
                }
               
            }
            else
            {
                obj1 = repo.GetEmployee(obj.EmoployeeId);
                obj1.EmployeeName = obj.EmployeeName;
                obj1.Email = obj.Email;
                obj1.JoinDate = obj.JoinDate;
                obj1.DesignationId = obj.DesignationId;
                string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                string extension = Path.GetExtension(obj.ImageFile.FileName);
                obj1.ImageName = fileName + extension;
                obj1.ImageUrl = "~/Images" + obj1.ImageName;
                fileName = Path.Combine(Server.MapPath(obj1.ImageUrl));
                obj.ImageFile.SaveAs(fileName);
                repo.UpdateEmployee(obj1);
                result = true;
                
            }

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<Designation> list = repo.GetDesignations();
                ViewBag.Designations = list;
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Delete(int id)
        {
           
            Employee emp = repo.GetEmployee(id);
            EmployeeViewModel obj = new EmployeeViewModel();
            obj.EmoployeeId = emp.EmployeeId;
            obj.EmployeeName = emp.EmployeeName;
            obj.Email = emp.Email;
            obj.ImageName = emp.ImageName;
            obj.JoinDate = emp.JoinDate;
            obj.ImageUrl = emp.ImageUrl;
            obj.DesignationId = emp.DesignationId;
            obj.DesignationTitle = emp.Designation.DesignationTitle;
            return View(obj);
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(EmployeeViewModel obj)
        {

            Employee emp = repo.GetEmployee(obj.EmoployeeId);
            repo.DeleteEmployee(emp.EmployeeId);
            return RedirectToAction("Index");

        }
        public PartialViewResult Details(int id)
        {
            List<Designation> list = repo.GetDesignations();
            ViewBag.Designations = list;
            Employee emp = repo.GetEmployee(id);
            EmployeeViewModel obj = new EmployeeViewModel();
            obj.EmoployeeId = emp.EmployeeId;
            obj.EmployeeName = emp.EmployeeName;
            obj.Email = emp.Email;
            obj.ImageName = emp.ImageName;
            obj.JoinDate = emp.JoinDate;
            obj.ImageUrl = emp.ImageUrl;
            obj.DesignationId = emp.DesignationId;
            obj.DesignationTitle = emp.Designation.DesignationTitle;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord",obj);
        }
        public ActionResult AjaxActionLintTest()
        {
            return View();
        }
        public PartialViewResult allEmployee()
        {
            
            using (EmployeeDBEntities1 db=new EmployeeDBEntities1())
            {
                System.Threading.Thread.Sleep(3000);
                List<Employee> list = db.Employees.ToList();
                return PartialView("_EmployeeList",list);
            }       
        }
        public PartialViewResult Top3Employees()
        {
            using (EmployeeDBEntities1 db = new EmployeeDBEntities1())
            {
                List<Employee> list = db.Employees.OrderBy(e=>e.EmployeeName).Take(3).ToList();
                return PartialView("_EmployeeList", list);
            }
        }
        public PartialViewResult bottom3Employees()
        {
            using (EmployeeDBEntities1 db = new EmployeeDBEntities1())
            {
                List<Employee> list = db.Employees.OrderByDescending(e => e.EmployeeName).Take(3).ToList();
                return PartialView("_EmployeeList", list);
            }
        }
        public ActionResult ExpertReport()
        {
            ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports")),"CrystalReport.rpt");
            string path = Server.MapPath("~") + "Reports//" + "Employee.rpt";
            rd.Load(path);
            List<EmployeeViewModel> list = repo.GetAllEmployees().ToList();
            //List<Employee>list=repo.GetAllEmployees().ToList();
            rd.SetDataSource(repo.GetAllEmployees().Select(s => new
            {
                ID = s.EmoployeeId,
                Name = s.EmployeeName,
                Email = s.Email,
                joinDate = s.JoinDate,
                designation =s.DesignationTitle,
                imageLink=s.ImageUrl

            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "apllication/pdf", "Employee.pdf");
        }
    }
}