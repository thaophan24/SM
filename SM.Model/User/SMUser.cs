using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SM.Model.User
{
    public class SMUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Description("User's permission, seperate by '|'")]
        public string UserPermission { get; set; }
    }
}
