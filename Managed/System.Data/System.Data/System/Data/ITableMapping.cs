using System;

namespace System.Data
{
	/// <summary>Associates a source table with a table in a <see cref="T:System.Data.DataSet" />, and is implemented by the <see cref="T:System.Data.Common.DataTableMapping" /> class, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CE RID: 206
	public interface ITableMapping
	{
		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection of data column mappings.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000BC0 RID: 3008
		IColumnMappingCollection ColumnMappings { get; }

		/// <summary>Gets or sets the case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000BC1 RID: 3009
		// (set) Token: 0x06000BC2 RID: 3010
		string DataSetTable { get; set; }

		/// <summary>Gets or sets the case-sensitive name of the source table.</summary>
		/// <returns>The case-sensitive name of the source table.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000BC3 RID: 3011
		// (set) Token: 0x06000BC4 RID: 3012
		string SourceTable { get; set; }
	}
}
