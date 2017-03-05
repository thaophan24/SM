using SM.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Business
{
    public interface IAuthBusiness
    {
        SMUser GetUser(string userName, string password);
    }
}
