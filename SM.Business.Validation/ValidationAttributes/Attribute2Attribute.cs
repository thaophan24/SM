using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Business.Validation.Attributes
{
    public class Attribute2Attribute : ValidationBaseAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return new ValidationResult("err from attribute 2 ");
        }
    }
}
