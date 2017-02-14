using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Security.Wcf
{
    public class AuthTokenHeader
    {
        public string UserName { get; set; }
        public string HashPassword { get; set; }
    }
}
