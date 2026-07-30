using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000136 RID: 310
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00005E03 File Offset: 0x00004003
		protected override IDbCommand BaseCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbCommand Command
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> class.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00050F85 File Offset: 0x0004F185
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(null, null, StatementType.Select, null)
		{
			throw ADP.OleDb();
		}
	}
}
