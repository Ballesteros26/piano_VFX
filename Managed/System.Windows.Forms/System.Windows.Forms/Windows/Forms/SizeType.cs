using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how rows or columns of user interface (UI) elements should be sized relative to their container.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002DF RID: 735
	public enum SizeType
	{
		/// <summary>The row or column should be automatically sized to share space with its peers.</summary>
		// Token: 0x040017B3 RID: 6067
		AutoSize,
		/// <summary>The row or column should be sized to an exact number of pixels.</summary>
		// Token: 0x040017B4 RID: 6068
		Absolute,
		/// <summary>The row or column should be sized as a percentage of the parent container.</summary>
		// Token: 0x040017B5 RID: 6069
		Percent
	}
}
