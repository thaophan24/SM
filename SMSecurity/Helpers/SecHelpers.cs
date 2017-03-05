using SM.Security.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SM.Security
{
    public static class SecHelpers
    {
        private static string _RsaPrivKey = null;
        private static string _RsaPublKey = null;
        public static string RsaPrivKey
        {
            get
            {
                if (_RsaPrivKey == null)
                {
                    _RsaPrivKey = System.IO.File.ReadAllText(
                        HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RSA_PRIV_KEY"]));
                }
                return _RsaPrivKey;
            }
        }
        public static string RsaPublicKey
        {
            get
            {
                if (_RsaPublKey == null)
                {
                    _RsaPublKey = System.IO.File.ReadAllText(
                        HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RSA_PUBL_KEY"]));
                }
                return _RsaPublKey;
            }
        }
        public static string WcfUser
        {
            get
            {
                return ConfigurationManager.AppSettings["WCF_USR"];
            }
        }
        public static string WcfPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["WCF_PWD"];
            }
        }
        public static bool IsAuthenticated(string usr, string hashedPwd)
        {
            var serv_usr = ConfigurationManager.AppSettings["WCF_USR"];
            var serv_pwd = ConfigurationManager.AppSettings["WCF_PWD"];
            var clearPwd = new RSACryptophy(RsaPrivKey).Decrypt(hashedPwd);
            return !string.IsNullOrEmpty(usr)
                && serv_usr.Equals(usr)
                && !string.IsNullOrEmpty(hashedPwd) 
                && serv_pwd.Equals(clearPwd);
        }
    }
}
