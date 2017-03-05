using SM.Common;
using SM.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SM.Web.AppCodes.Authorization
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private const char SEPERATE_CHAR = '|';
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool res = false;
            if (SessionManager.CurrentUser != null)
            {
                if (!string.IsNullOrEmpty(SessionManager.CurrentUser.UserPermission))
                {
                    string[] permissions = SessionManager.CurrentUser.UserPermission.Split(SEPERATE_CHAR);
                    string[] roles = this.Roles.Split(SEPERATE_CHAR);
                    foreach (var item in roles)
                    {
                        res = permissions.Contains(item);
                        if (!res) { break; }
                    }
                }
            }
            return res;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}