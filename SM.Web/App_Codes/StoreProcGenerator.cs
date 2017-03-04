//using System.IO;
//using System.Web;
//using SM.Business; 
//public class StoreProcGenerator
//{
//	private static string InsertTemplate
//	{
//		get
//		{
//			var path = HttpContext.Current.Server.MapPath("~/App_Data/insert.txt");
//			return File.ReadAllText(path);
//		}
//	}
//	private static string UpdateTemplate
//	{
//		get
//		{
//			var path = HttpContext.Current.Server.MapPath("~/App_Data/update.txt");
//			return File.ReadAllText(path);
//		}
//	}
//	private static string DeleteTemplate
//	{
//		get
//		{
//			var path = HttpContext.Current.Server.MapPath("~/App_Data/delete.txt");
//			return File.ReadAllText(path);
//		}
//	}
//	public static string CreateDefault(object model)
//	{
//        string tableName = model.GetType().GetRepresentTable();
//		var dt = ServiceProxy.GetTableColumnsType(tableName);
//		var columnTypes = dt.ToEnumerable<ColumnType>();
//		var properties = model.GetProperties();
//		// just process not identity column
//		var notIdentityProps = properties.Where(x => x.GetCustomAttribute<IdentityAttribute>() == null);
//		var notIdentityColumns = columnTypes.Where(x => notIdentityColumns.Any(y => y.Name == x.Name));
//		// generate store proc content
//		string parameters = GenerateParams(notIdentityColumns, model);
//		string cols = GenerateColumns(properties);
//		string values = GenerateParams(notIdentityColumns, model, false);
//		string res = string.Format(InsertTemplate, tableName, parameters, tableName, cols, values);
//		return res;
//	}
//	public static string UpdateDefault(object model)
//	{
//		string tableName = model.GetRepresentTable();
//		var dt = ServiceProxy.GetTableColumnsType(tableName);
//		var columnTypes = dt.ToEnumerable<ColumnType>();
//		var properties = model.GetProperties();
//		// just process not key column
//		var notKeyProps = properties.Where(x => x.GetCustomAttribute<KeyAttribute>() == null);
//		var keyColumn = properties.Except(notKeyProps);
//		string parameters = GenerateParams(columnTypes, model);
//		string values = GenerateUpdateValues(notKeyProps.ToArray(), model);
//		var keyProps = new List<PropertyInfo>(){keyColumn};
//		string whereCondition = GenerateWhereCondition(keyProps);
//		string res = string.Format(UpdateTemplate, tableName, parameters, tableName, values, whereCondition);
//		return res;
//	}
//	public static string DeleteDefault(object model)
//	{
//		string tableName = model.GetRepresentTable();
//		var dt = ServiceProxy.GetTableColumnsType(tableName);
//		var columnTypes = dt.ToEnumerable<ColumnType>();
//		var keyColumn = model.GetType().GetFirstPropertyAppliedAttribute<KeyAttribute>();
//		var keyProps = new List<PropertyInfo>(){keyColumn};
//		string parameters = GenerateParams(columnTypes.Where(x => x.Name == keyColumn.Name), model);
//		string whereCondition = GenerateWhereCondition(keyProps);
//		string res = string.Format(DeleteTemplate, tableName, parameters, tableName, whereCondition);
//		return res;
//	}
	
//	private static string GenerateParams(IEnumerable<ColumnType> columnTypes, object model, bool withType = true)
//	{
//		string res = string.Empty;
//		foreach (ColumnType ct in columnTypes)
//		{
//			res += "@" + ct.Name + " ";
//			if (withType)
//			{
//				res += ct.Type;
//				if (ct.Length.HasValue)
//				{
//					res += "(" + ct.Length + ")";
//				}
//			}
//			res += ", ";
//		}
//		return res.Trim().Trim(',');
//	}
//	private static string GenerateColumns(PropertyInfo[] props)
//	{
//		string res = string.Empty;
//		foreach (PropertyInfo prop in props)
//		{
//			res += prop.Name + ",";
//		}
//		return res.Trim(',');
//	}
//	private static string GenerateInsertValues(PropertyInfo[] props, object model)
//	{
//		string res = string.Empty;
//		foreach (PropertyInfo prop in props)
//		{
//			if (prop.PropertyType == typeof(DateTime) || 
//				prop.PropertyType == typeof(string))
//				{
//					res += "'" + prop.GetValue(model) + "',";
//				}
//				else 
//				{
//					res += prop.GetValue(model) + ",";
//				}
//		}
//		return res.Trim(',');
//	}
//	private static string GenerateUpdateValues(PropertyInfo[] props, object model)
//	{
//		string res = string.Empty;
//		foreach(PropertyInfo prop in props)
//		{
//			if (prop.PropertyType == typeof(DateTime) || 
//				prop.PropertyType == typeof(string))
//				{
//					res += prop.Name + " = " + "'" + prop.GetValue(model) + "'";
//				}
//				else 
//				{
//					res += prop.Name + " = " + prop.GetValue(model);
//				}
//			res += ", ";
//		}
//		return res.Trim().Trim(',');
//	}
//	private static string GenerateWhereCondition(IEnumerable<PropertyInfo> props)
//	{
//		string res = string.Empty;
//		foreach (PropertyInfo prop in props)
//		{
//			res += prop.Name + " = @" + prop.Name + " and ";
//		}
//		return res.Trim().Trim("and");
//	}
//}