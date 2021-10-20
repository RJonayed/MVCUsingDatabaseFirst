using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Models.ViewModels
{
    public class CompanyView
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public string Company1 { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public System.DateTime EstDate { get; set; }
    }
}