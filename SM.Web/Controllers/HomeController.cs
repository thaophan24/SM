using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SM.Web.Controllers
{
    public class Model
    {
        public string Value { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new Service.Service1Client();
            service.ClientCredentials.UserName.UserName = "sm";
            //service.ClientCredentials.UserName.Password = "sm";
            string s = service.GetData();
            return View(new Model() { Value = s });
        }
    }
}