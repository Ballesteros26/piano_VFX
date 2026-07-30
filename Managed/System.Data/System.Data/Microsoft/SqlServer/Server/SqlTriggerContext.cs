using System;
using System.Data.Common;
using System.Data.SqlTypes;
using Unity;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Provides contextual information about the trigger that was fired. </summary>
	// Token: 0x020003B3 RID: 947
	public sealed class SqlTriggerContext
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x000C8121 File Offset: 0x000C6321
		internal SqlTriggerContext(TriggerAction triggerAction, bool[] columnsUpdated, SqlXml eventInstanceData)
		{
			this._triggerAction = triggerAction;
			this._columnsUpdated = columnsUpdated;
			this._eventInstanceData = eventInstanceData;
		}

		/// <summary>Gets the number of columns contained by the data table bound to the trigger. This property is read-only.</summary>
		/// <returns>The number of columns contained by the data table bound to the trigger, as an integer. </returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000C8140 File Offset: 0x000C6340
		public int ColumnCount
		{
			get
			{
				int num = 0;
				if (this._columnsUpdated != null)
				{
					num = this._columnsUpdated.Length;
				}
				return num;
			}
		}

		/// <summary>Gets the event data specific to the action that fired the trigger.</summary>
		/// <returns>The event data specific to the action that fired the trigger as a <see cref="T:System.Data.SqlTypes.SqlXml" /> if more information is available; null otherwise.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000C8161 File Offset: 0x000C6361
		public SqlXml EventData
		{
			get
			{
				return this._eventInstanceData;
			}
		}

		/// <summary>Indicates what action fired the trigger.</summary>
		/// <returns>The action that fired the trigger as a <see cref="T:Microsoft.SqlServer.Server.TriggerAction" />.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000C8169 File Offset: 0x000C6369
		public TriggerAction TriggerAction
		{
			get
			{
				return this._triggerAction;
			}
		}

		/// <summary>Returns true if a column was affected by an INSERT or UPDATE statement.</summary>
		/// <returns>true if the column was affected by an INSERT or UPDATE operation.</returns>
		/// <param name="columnOrdinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.InvalidOperationException">Called in the context of a trigger where the value of the <see cref="P:Microsoft.SqlServer.Server.SqlTriggerContext.TriggerAction" /> property is not Insert or Update.</exception>
		// Token: 0x06002DEE RID: 11758 RVA: 0x000C8171 File Offset: 0x000C6371
		public bool IsUpdatedColumn(int columnOrdinal)
		{
			if (this._columnsUpdated != null)
			{
				return this._columnsUpdated[columnOrdinal];
			}
			throw ADP.IndexOutOfRange(columnOrdinal);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x00010468 File Offset: 0x0000E668
		internal SqlTriggerContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B2E RID: 6958
		private TriggerAction _triggerAction;

		// Token: 0x04001B2F RID: 6959
		private bool[] _columnsUpdated;

		// Token: 0x04001B30 RID: 6960
		private SqlXml _eventInstanceData;
	}
}
