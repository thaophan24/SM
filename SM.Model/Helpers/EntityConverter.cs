using SM.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SM.Model.Helpers
{
    public static class EntityConverter
    {
        public static T To<T>(this DataRow row) where T : class, new()
        {
            Type t = typeof(T);
            PropertyInfo[] props = CacheManager.Instance.GetCachedProperties(t);
            T res = new T();
            foreach (var prop in props)
            {
                string colName = prop.Name;
                var columnAtt = prop.GetCustomAttribute<MapColumnAttribute>();
                if (columnAtt != null)
                {
                    colName = columnAtt.Name;
                }
                if (!row.IsNull(colName))
                {
                    t.InvokeMember(prop.Name, BindingFlags.SetProperty, null, res, new object[] { row[colName] });
                }
            }
            return res;
        }
        public static IEnumerable<T> ToEnumerable<T>(this DataTable table) where T : class, new()
        {
            if (table != null && table.Rows.Count != 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    yield return row.To<T>();
                }
            }
        }
    }
}