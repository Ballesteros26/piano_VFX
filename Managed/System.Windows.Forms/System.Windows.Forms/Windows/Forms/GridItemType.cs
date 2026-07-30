using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the valid grid item types for a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A7 RID: 423
	public enum GridItemType
	{
		/// <summary>A grid entry that corresponds to a property.</summary>
		// Token: 0x04000F1B RID: 3867
		Property,
		/// <summary>A grid entry that is a category name. A category is a descriptive grouping for groups of <see cref="T:System.Windows.Forms.GridItem" /> rows. Typical categories include the following Behavior, Layout, Data, and Appearance.</summary>
		// Token: 0x04000F1C RID: 3868
		Category,
		/// <summary>The <see cref="T:System.Windows.Forms.GridItem" /> is an element of an array.</summary>
		// Token: 0x04000F1D RID: 3869
		ArrayValue,
		/// <summary>A root item in the grid hierarchy.</summary>
		// Token: 0x04000F1E RID: 3870
		Root
	}
}
