using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Model.User;
using SM.Model;

namespace SM.Repository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public SMUser GetUser(string userName, string password)
        {
            var tbl = ServiceProxy.GetUser(userName, password);
            SMUser user = null;
            if (tbl.Rows.Count != 0)
            {
                user = tbl.Rows[0].To<SMUser>();
            }
            return user;
        }
    }
}
