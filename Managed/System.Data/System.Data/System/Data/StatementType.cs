using System;

namespace System.Data
{
	/// <summary>Specifies the type of SQL query to be used by the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" />, <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" />, <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" />, or <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FB RID: 251
	public enum StatementType
	{
		/// <summary>An SQL query that is a SELECT statement.</summary>
		// Token: 0x040008A3 RID: 2211
		Select,
		/// <summary>An SQL query that is an INSERT statement.</summary>
		// Token: 0x040008A4 RID: 2212
		Insert,
		/// <summary>An SQL query that is an UPDATE statement.</summary>
		// Token: 0x040008A5 RID: 2213
		Update,
		/// <summary>An SQL query that is a DELETE statement.</summary>
		// Token: 0x040008A6 RID: 2214
		Delete,
		/// <summary>A SQL query that is a batch statement.</summary>
		// Token: 0x040008A7 RID: 2215
		Batch
	}
}
