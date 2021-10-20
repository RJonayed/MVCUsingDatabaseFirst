using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Models.ViewModels
{
    public class DesignationViewModel
    {
        [Display (Name ="Designation")]
        public int DesignationId { get; set; }
        [Display(Name ="Designation Title")]
        public string DesignationTitle { get; set; }
    }
}