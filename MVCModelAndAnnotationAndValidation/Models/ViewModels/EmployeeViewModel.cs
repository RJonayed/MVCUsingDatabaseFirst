using MVCModelAndAnnotationAndValidation.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCModelAndAnnotationAndValidation.Models.ViewModels
{
    [MetadataType(typeof(EmployeeMataData))]
    public class EmployeeViewModel
    {
        [Display(Name ="Employee ID")]
        [Key]
        public int EmoployeeId { get; set; }
        [Display(Name = "Employee Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Employee Name Required")]
        [Remote("IsEmployeeNameAvailable", "Employees", ErrorMessage = "Employee Name already is in use")]
        public string EmployeeName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email is required")]
        //  [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",ErrorMessage ="Email is not Valid")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email is not valid")]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Join Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ValidateJoinDate]
        public DateTime JoinDate { get; set; }
      
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public  HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage ="Please Select Designation")]
        public int DesignationId { get; set; }

        public string PageTitle { get; set; }
        public string DesignationTitle { get; set; }

        //[Range(20,60,ErrorMessage ="Age Range Must be within 20-60")]
        ////public int Age { get; set; }
        ////[StringLength(10,ErrorMessage ="Length Must be within 10 Charcter")]
        ////public string Password { get; set; }
        ////[Compare("Password",ErrorMessage ="Password and ConfimPassword must match")]
        ////public string ConfirmPassword { get; set; }
    }
}