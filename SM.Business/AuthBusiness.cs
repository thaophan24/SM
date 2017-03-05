using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Model.User;
using SM.Repository;

namespace SM.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        protected IAuthRepository AuthRepo { get; set; }
        public AuthBusiness()
        {
            AuthRepo = new AuthRepository();
        }
        public SMUser GetUser(string userName, string password)
        {
            return AuthRepo.GetUser(userName, password);
        }
    }
}
