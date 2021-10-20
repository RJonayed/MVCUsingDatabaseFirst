using MVCModelAndAnnotationAndValidation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCModelAndAnnotationAndValidation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            CerationInititalUserRole();
            //RepoDependency();
        }

        private void CerationInititalUserRole()
        {
            EmployeeDBEntities1 dbObj = new EmployeeDBEntities1();
            using (var dbOb = new EmployeeDBEntities1()) 
            {
                int count = dbOb.TblUsers.Count();
                if (count == 0)
                {
                    TblUser user = new TblUser();
                    user.UserName = "Admin";
                    user.Password_ = "123";
                }
            }
        }

        protected void Application_Error()
        {

        }
    }
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
