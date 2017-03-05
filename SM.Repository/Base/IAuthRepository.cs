using SM.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Repository
{
    public interface IAuthRepository
    {
        SMUser GetUser(string userName, string password);
    }
}
