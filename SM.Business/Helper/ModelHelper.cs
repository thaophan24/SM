using System;
using System.Linq;
namespace SM.Business
{
    public static class ModelHelper
    {
        public static string GetRepresentTable(this Type type)
        {
            string res = string.Empty;
            var atts = type.GetCustomAttributes(typeof(MapTableAttribute), false);
            if (atts != null && atts.Any())
            {
                var tblAttr = atts.First() as MapTableAttribute;
                res = tblAttr.Name;
            }
            return res;
        }
    }
}