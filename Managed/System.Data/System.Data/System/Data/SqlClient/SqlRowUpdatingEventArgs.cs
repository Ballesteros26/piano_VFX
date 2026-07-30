using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DD RID: 477
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x0600163B RID: 5691 RVA: 0x0006EB5A File Offset: 0x0006CD5A
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0006EB67 File Offset: 0x0006CD67
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0006EB74 File Offset: 0x0006CD74
		public new SqlCommand Command
		{
			get
			{
				return base.Command as SqlCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0006EB7D File Offset: 0x0006CD7D
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0006EB85 File Offset: 0x0006CD85
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as SqlCommand;
			}
		}
	}
}
