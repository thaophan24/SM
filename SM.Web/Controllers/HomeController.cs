using SM.Business.ValidationAttributes;
using SM.Model;
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
        public ActionResult Index()
        {
            Model1 m = new Model1() { Id = 123, Name = "asdf" };
            return View(m);
        }
        [HttpPost]
        public ActionResult Index(Model1 m)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            var props = m.GetType().GetProperties();
            foreach (var prop in props)
            {
                var validators = prop.GetCustomAttributes(false)
                    .ToList()
                    .Where(x => x is ValidationBaseAttribute)
                    .OrderBy(x => ((ValidationBaseAttribute)x).Order);
                foreach (ValidationAttribute validator in validators)
                {
                    res.Add(validator.GetValidationResult(prop.GetValue(m), new ValidationContext(m)));
                }
            }
            foreach (ValidationResult result in res)
            {
                Response.Write(result.ErrorMessage + "<br/>");
            }
            //ValidationContext ctx = new ValidationContext(m, null, null)
            //{
            //   MemberName = "Id"
            //};
            //var valRes = new List<ValidationResult>();
            //bool isValid = Validator.TryValidateProperty(m.Id, ctx, valRes);
            //m.Name = isValid ? "success" : "failed";
            //valRes.ForEach(x => Response.Write(x + "<br/>"));
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Model1));
            //foreach (PropertyDescriptor prop in properties)
            //{
            //    Response.Write(prop.Name + " :<br/>");
            //    foreach(Attribute att in prop.Attributes)
            //    {
            //        if (att.ToString().Contains("SM.Business") || att.ToString().Contains("DataAnnotations"))
            //        {
            //            Response.Write(att.ToString() + "<br/>");
            //        }
            //    }
            //}
            return View(m);
        }
    }
}