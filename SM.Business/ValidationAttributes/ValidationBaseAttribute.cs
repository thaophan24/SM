using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Business.ValidationAttributes
{
    public class ValidationBaseAttribute : ValidationAttribute
    {
        public int Order { get; set; }
    }
}
