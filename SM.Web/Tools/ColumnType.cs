using System;

namespace SM.Web.Tools
{
    public class ColumnType
    {
        [MapColumn(Name = "COLUMN_NAME")]
        public string Name { get; set; }
        [MapColumn(Name = "DATA_TYPE")]
        public string Type { get; set; }
        [MapColumn(Name = "CHARACTER_MAXIMUM_LENGTH")]
        public Nullable<int> Length { get; set; }
    }
}