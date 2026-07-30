using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.RowChanged" />, <see cref="E:System.Data.DataTable.RowChanging" />, <see cref="M:System.Data.DataTable.OnRowDeleting(System.Data.DataRowChangeEventArgs)" />, and <see cref="M:System.Data.DataTable.OnRowDeleted(System.Data.DataRowChangeEventArgs)" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007E RID: 126
	public class DataRowChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRowChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> upon which an action is occuring. </param>
		/// <param name="action">One of the <see cref="T:System.Data.DataRowAction" /> values. </param>
		// Token: 0x06000642 RID: 1602 RVA: 0x00019856 File Offset: 0x00017A56
		public DataRowChangeEventArgs(DataRow row, DataRowAction action)
		{
			this.Row = row;
			this.Action = action;
		}

		/// <summary>Gets the row upon which an action has occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> upon which an action has occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001986C File Offset: 0x00017A6C
		public DataRow Row { get; }

		/// <summary>Gets the action that has occurred on a <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowAction" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00019874 File Offset: 0x00017A74
		public DataRowAction Action { get; }
	}
}
