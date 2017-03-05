using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SM.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name ="Your User Name:")]
        public string UserName { get; set; }
        [Display(Name = "Your Password:")]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}