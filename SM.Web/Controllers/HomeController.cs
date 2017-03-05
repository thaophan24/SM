using SM.Business.Validation.Attributes;
using SM.Model;
using SM.Web.AppCodes.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SM.Web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(Roles = "Fuck")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Person person)
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}