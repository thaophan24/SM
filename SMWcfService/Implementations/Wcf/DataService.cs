using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SM.WcfService.Implementations
{
    public class DataService : IDataService
    {
        public DataTable GetTableColumnsType(string tableName)
        {
            return DbManager.ExecuteQuery(string.Format(
            "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'"
            , tableName));
        }

        public DataTable GetUser(string userName, string password)
        {
            return DbManager.ExecuteQuery(
                string.Format(
                    "select * from SMUser where UserName = '{0}' and Password = '{1}'",
                userName, password));
        }
    }
}