using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Models.ViewModels
{
    public class UserRoleViewModel
    {
        [Display(Name ="Role Id")]
        public int RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}