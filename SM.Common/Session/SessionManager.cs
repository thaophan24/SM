using SM.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SM.Common
{
    public class SessionManager
    {
        public static SMUser CurrentUser
        {
            get
            {
                SMUser usr = null;
                if (HttpContext.Current.Session[SessionKeys.CurrentUser] != null)
                {
                    usr = (SMUser)HttpContext.Current.Session[SessionKeys.CurrentUser];
                }
                return usr;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.CurrentUser] = value;
            }
        }
    }
}
