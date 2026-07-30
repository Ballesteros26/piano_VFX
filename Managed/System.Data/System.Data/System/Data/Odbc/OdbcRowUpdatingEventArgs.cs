using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AE RID: 686
	public sealed class OdbcRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to update. </param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute during the update operation. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001D5A RID: 7514 RVA: 0x0006EB5A File Offset: 0x0006CD5A
		public OdbcRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0009122F File Offset: 0x0008F42F
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0006EB74 File Offset: 0x0006CD74
		public new OdbcCommand Command
		{
			get
			{
				return base.Command as OdbcCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0006EB7D File Offset: 0x0006CD7D
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0009123C File Offset: 0x0008F43C
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as OdbcCommand;
			}
		}
	}
}
