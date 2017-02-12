using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.DataAccessing
{
    public class SMDatabase : SqlDatabase
    {
        public SMDatabase(string connString) : base(connString)
        {
        } 
        public DataTable ExecuteDataTable(string procName, SMParameterCollection parameters)
        {
            DbCommand cmd = this.GetStoredProcCommand(procName);
            foreach (var param in parameters)
            {
                cmd.Parameters[param.Name].Value = param.Value;
                cmd.Parameters[param.Name].DbType = param.DbType;
            }
            var dataSet = this.ExecuteDataSet(cmd);
            DataTable dt = dataSet.Tables.Count == 0 ? new DataTable() : dataSet.Tables[0];
            return dt;
        }
    }
}
