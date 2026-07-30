using System;

namespace System.Data
{
	/// <summary>Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and update a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CB RID: 203
	public interface IDbDataAdapter : IDataAdapter
	{
		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000BAE RID: 2990
		// (set) Token: 0x06000BAF RID: 2991
		IDbCommand SelectCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000BB0 RID: 2992
		// (set) Token: 0x06000BB1 RID: 2993
		IDbCommand InsertCommand { get; set; }

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000BB2 RID: 2994
		// (set) Token: 0x06000BB3 RID: 2995
		IDbCommand UpdateCommand { get; set; }

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000BB4 RID: 2996
		// (set) Token: 0x06000BB5 RID: 2997
		IDbCommand DeleteCommand { get; set; }
	}
}
