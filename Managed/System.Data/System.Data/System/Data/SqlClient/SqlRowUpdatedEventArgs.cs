using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DB RID: 475
	public sealed class SqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</param>
		// Token: 0x06001635 RID: 5685 RVA: 0x0006EB40 File Offset: 0x0006CD40
		public SqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0006EB4D File Offset: 0x0006CD4D
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
		}
	}
}
