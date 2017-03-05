using SM.Repository.DataService;
using SM.Security;
using SM.Security.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Repository
{
    public class BaseRepository
    {
        private DataServiceClient _ServiceProxy = null;
        public DataServiceClient ServiceProxy
        {
            get
            {
                if (_ServiceProxy == null)
                {
                    _ServiceProxy = new DataServiceClient();
                    _ServiceProxy.ClientCredentials.UserName.UserName = SecHelpers.WcfUser;
                    RSACryptophy crypto = new RSACryptophy(SecHelpers.RsaPublicKey);
                    var hashedPwd = crypto.Encrypt(SecHelpers.WcfPassword);
                    _ServiceProxy.ClientCredentials.UserName.Password = hashedPwd;
                }
                return _ServiceProxy;
            }
        }
    }
}
