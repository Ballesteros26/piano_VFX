using System;

namespace System.Data
{
	/// <summary>Provides a means of reading one or more forward-only streams of result sets obtained by executing a command at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C7 RID: 199
	public interface IDataReader : IDisposable, IDataRecord
	{
		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The level of nesting.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B6F RID: 2927
		int Depth { get; }

		/// <summary>Gets a value indicating whether the data reader is closed.</summary>
		/// <returns>true if the data reader is closed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B70 RID: 2928
		bool IsClosed { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B71 RID: 2929
		int RecordsAffected { get; }

		/// <summary>Closes the <see cref="T:System.Data.IDataReader" /> Object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B72 RID: 2930
		void Close();

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.IDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B73 RID: 2931
		DataTable GetSchemaTable();

		/// <summary>Advances the data reader to the next result, when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B74 RID: 2932
		bool NextResult();

		/// <summary>Advances the <see cref="T:System.Data.IDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B75 RID: 2933
		bool Read();
	}
}
