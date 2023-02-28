namespace Travaloud.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DapperColumnMapAttribute : Attribute
	{
		public string ColumnName { get; set; }
		public DapperColumnMapAttribute(string columnName)
		{
			ColumnName = columnName;
		}
	}
}

