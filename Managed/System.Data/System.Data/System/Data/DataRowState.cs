using System;

namespace System.Data
{
	/// <summary>Gets the state of a <see cref="T:System.Data.DataRow" /> object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000084 RID: 132
	[Flags]
	public enum DataRowState
	{
		/// <summary>The row has been created but is not part of any <see cref="T:System.Data.DataRowCollection" />. A <see cref="T:System.Data.DataRow" /> is in this state immediately after it has been created and before it is added to a collection, or if it has been removed from a collection.</summary>
		// Token: 0x0400059B RID: 1435
		Detached = 1,
		/// <summary>The row has not changed since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		// Token: 0x0400059C RID: 1436
		Unchanged = 2,
		/// <summary>The row has been added to a <see cref="T:System.Data.DataRowCollection" />, and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x0400059D RID: 1437
		Added = 4,
		/// <summary>The row was deleted using the <see cref="M:System.Data.DataRow.Delete" /> method of the <see cref="T:System.Data.DataRow" />.</summary>
		// Token: 0x0400059E RID: 1438
		Deleted = 8,
		/// <summary>The row has been modified and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x0400059F RID: 1439
		Modified = 16
	}
}
