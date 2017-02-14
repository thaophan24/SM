public class DataService : IDataService
{
	public DataTable GetTableColumnsType(string tableName)
	{
		return DataManager.Db.GetData(string.Format(
		"select COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{0}'"
		,tableName));
	}
}