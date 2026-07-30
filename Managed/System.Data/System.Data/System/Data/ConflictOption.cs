using System;

namespace System.Data
{
	/// <summary>Specifies how conflicting changes to the data source will be detected and resolved.</summary>
	// Token: 0x02000055 RID: 85
	public enum ConflictOption
	{
		/// <summary>Update and delete statements will include all searchable columns from the table in the WHERE clause. This is equivalent to specifying CompareAllValuesUpdate | CompareAllValuesDelete.</summary>
		// Token: 0x040004FB RID: 1275
		CompareAllSearchableValues = 1,
		/// <summary>If any Timestamp columns exist in the table, they are used in the WHERE clause for all generated update statements. This is equivalent to specifying CompareRowVersionUpdate | CompareRowVersionDelete.</summary>
		// Token: 0x040004FC RID: 1276
		CompareRowVersion,
		/// <summary>All update and delete statements include only <see cref="P:System.Data.DataTable.PrimaryKey" /> columns in the WHERE clause. If no <see cref="P:System.Data.DataTable.PrimaryKey" /> is defined, all searchable columns are included in the WHERE clause. This is equivalent to OverwriteChangesUpdate | OverwriteChangesDelete.</summary>
		// Token: 0x040004FD RID: 1277
		OverwriteChanges
	}
}
