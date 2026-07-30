using System;

namespace System.Data
{
	/// <summary>Describes an action performed on a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007D RID: 125
	[Flags]
	public enum DataRowAction
	{
		/// <summary>The row has not changed.</summary>
		// Token: 0x0400058D RID: 1421
		Nothing = 0,
		/// <summary>The row was deleted from the table.</summary>
		// Token: 0x0400058E RID: 1422
		Delete = 1,
		/// <summary>The row has changed.</summary>
		// Token: 0x0400058F RID: 1423
		Change = 2,
		/// <summary>The most recent change to the row has been rolled back.</summary>
		// Token: 0x04000590 RID: 1424
		Rollback = 4,
		/// <summary>The changes to the row have been committed.</summary>
		// Token: 0x04000591 RID: 1425
		Commit = 8,
		/// <summary>The row has been added to the table.</summary>
		// Token: 0x04000592 RID: 1426
		Add = 16,
		/// <summary>The original version of the row has been changed.</summary>
		// Token: 0x04000593 RID: 1427
		ChangeOriginal = 32,
		/// <summary>The original and the current versions of the row have been changed.</summary>
		// Token: 0x04000594 RID: 1428
		ChangeCurrentAndOriginal = 64
	}
}
