using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCModelAndAnnotationAndValidation.Repositories
{
    public class ValidateJoinDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            DateTime CurrentDate = DateTime.Now;
           
            string message = string.Empty;
            if (Convert.ToDateTime(value).Date>=CurrentDate.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                message = "Join date cannot be less than current Date";
                return new ValidationResult(message);
               
            }
        }
    }
}