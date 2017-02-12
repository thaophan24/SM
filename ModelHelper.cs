public static class ModelHelper
{
	public static string GetRepresentTable(this object model)
	{
		string res = string.Empty;
		var atts = model.GetType().GetCustomAttributes(typeof(MapTableAttribute), false);
		if (atts != null && atts.Any())
		{
			var tblAttr = atts.First() as MapTableAttribute;
			res = tblAttr.Name;
		}
		return res;
	}
}