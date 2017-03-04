using SM.Security.Wcf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SM.WcfService.Implementations
{
    [ServiceContract]
    [SecuredService]
    public interface IDataService
    {
        [OperationContract]
        DataTable GetTableColumnsType(string tableName);
    }
}
