using SM.Security.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SM.Security.Wcf
{
    public static class SecHelpers
    {
        public static string RsaPrivKey
        {
            get
            {
                return HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RSA_PRIV_KEY"]);
            }
        }
        public static bool IsAuthenticated(string usr, string hashedPwd)
        {
            var serv_usr = ConfigurationManager.AppSettings["WCF_USR"];
            var serv_pwd = ConfigurationManager.AppSettings["WCF_PWD"];
            var clearPwd = new RSACryptophy(RsaPrivKey).Decrypt(hashedPwd);
            return serv_usr.Equals(usr) && serv_pwd.Equals(clearPwd);
        }
    }
}
