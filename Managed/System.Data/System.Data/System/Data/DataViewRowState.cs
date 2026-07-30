using System;

namespace System.Data
{
	/// <summary>Describes the version of data in a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200009E RID: 158
	[Flags]
	public enum DataViewRowState
	{
		/// <summary>None.</summary>
		// Token: 0x04000674 RID: 1652
		None = 0,
		/// <summary>An unchanged row.</summary>
		// Token: 0x04000675 RID: 1653
		Unchanged = 2,
		/// <summary>A new row.</summary>
		// Token: 0x04000676 RID: 1654
		Added = 4,
		/// <summary>A deleted row.</summary>
		// Token: 0x04000677 RID: 1655
		Deleted = 8,
		/// <summary>A current version of original data that has been modified (see ModifiedOriginal).</summary>
		// Token: 0x04000678 RID: 1656
		ModifiedCurrent = 16,
		/// <summary>The original version of the data that was modified. (Although the data has since been modified, it is available as ModifiedCurrent).</summary>
		// Token: 0x04000679 RID: 1657
		ModifiedOriginal = 32,
		/// <summary>Original rows including unchanged and deleted rows.</summary>
		// Token: 0x0400067A RID: 1658
		OriginalRows = 42,
		/// <summary>Current rows including unchanged, new, and modified rows. By default, <see cref="T:System.Data.DataViewRowState" /> is set to CurrentRows.</summary>
		// Token: 0x0400067B RID: 1659
		CurrentRows = 22
	}
}
