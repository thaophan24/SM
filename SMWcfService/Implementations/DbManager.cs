using SM.DataAccessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SM.Model;
using SM.Cache;
using System.Reflection;
using System.Data;

namespace SM.WcfService.Implementations
{
    public class DbManager
    {
        private static SMDatabase Database { get; set; }
        private const string InsertStoreTemplate = "Insert{0}";
        private const string UpdateStoreTemplate = "Update{0}";
        private const string DeleteStoreTemplate = "Delete{0}";

        private DbManager()
        {
            var connStr = ConfigurationManager.ConnectionStrings["DB_CONNSTRING"].ConnectionString;
            Database = new SMDatabase(connStr);
        }
        private static string GetObjectDbTable<T>(T obj) where T : class
        {
            string table = CacheManager.Instance.GetCachedDbTable(typeof(T));
            if (string.IsNullOrEmpty(table))
            {
                table = obj.GetRepresentTable();
                if (string.IsNullOrEmpty(table))
                {
                    table = obj.GetType().Name;
                }
                CacheManager.Instance.SetCacheDbTable(typeof(T), table);
            }
            return table;
        }
        private static object[] SerializeParamValues<T>(T obj)
        {
            Type objType = typeof(T);
            PropertyInfo[] props = CacheManager.Instance.GetCachedProperties(objType);
            if (props == null)
            {
                props = objType.GetProperties();
                CacheManager.Instance.SetCacheProperties(objType);
            }
            List<object> values = new List<object>();
            foreach(PropertyInfo p in props)
            {
                if (!p.IsDefined(typeof(IdentityAttribute)))
                {
                    values.Add(p.GetValue(obj));
                }
            }
            return values.ToArray();
        }
        public static int Insert<T>(T obj) where T : class
        {
            var table = GetObjectDbTable<T>(obj);
            var values = SerializeParamValues<T>(obj);
            var store = string.Format(InsertStoreTemplate, table);
            return Database.ExecuteNonQuery(store, values);
        }
        public static DataTable ExecuteQuery(string query)
        {
            var command = Database.GetSqlStringCommand(query);
            var ds = Database.ExecuteDataSet(command);
            return ds.Tables.Count != 0 ? ds.Tables[0] : new DataTable();
        }
    }
}