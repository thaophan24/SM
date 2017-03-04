using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SM.WcfService.Implementations
{
    public class DataService
    {
        public DataTable GetTableColumnsType(string tableName)
        {
            return DbManager.ExecuteQuery(string.Format(
            "select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'"
            , tableName));
        }
    }
}