using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SM.Business;
using SM.Common;
using SM.Model.User;
using SM.Web.AppCodes.Authorization;
using SM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace SM.Web.Controllers
{
    public class AccountController : BaseController
    {
        protected IAuthBusiness AuthBusiness { get; set; }
        protected IAuthenticationManager AuthManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }
        public AccountController(IAuthBusiness bus)
        {
            this.AuthBusiness = bus;
        }
        private void SignIn(SMUser user)
        {
            SessionManager.CurrentUser = user;
            var claims = new List<Claim>();
            claims.Add(new Claim(user.UserId, user.UserName));
            ClaimsIdentity identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = false,
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.AddSeconds(15)
            }, identity);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = AuthBusiness.GetUser(loginModel.UserName, loginModel.Password);
                if (user != null) 
                {
                    // sign in user
                    SignIn(user);
                    // redirect
                    if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    loginModel.ErrorMessage = "Invalid User Name or Password";
                }
            }
            return View(loginModel);
        }
    }
}