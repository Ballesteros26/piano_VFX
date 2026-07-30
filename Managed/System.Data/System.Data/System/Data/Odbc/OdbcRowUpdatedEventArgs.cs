using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AF RID: 687
	public sealed class OdbcRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatedEventArgs" /> class.</summary>
		/// <param name="row">The DataRow sent through an update operation. </param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001D5F RID: 7519 RVA: 0x0006EB40 File Offset: 0x0006CD40
		public OdbcRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0009124A File Offset: 0x0008F44A
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
		}
	}
}
