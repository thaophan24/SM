using System.Linq;

public static class ModelHelper
{
	public static string GetRepresentTable<T>()
	{
		string res = string.Empty;
		var atts = typeof(T).GetType().GetCustomAttributes(typeof(MapTableAttribute), false);
		if (atts != null && atts.Any())
		{
			var tblAttr = atts.First() as MapTableAttribute;
			res = tblAttr.Name;
		}
		return res;
	}
}