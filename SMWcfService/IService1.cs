using SM.Security.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SMWcfService
{
    [ServiceContract]
    [SecuredService]
    public interface IService1
    {
        [OperationContract]
        string GetData();
    }
}
