using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the text orientation to use with a particular <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000382 RID: 898
	public enum ToolStripTextDirection
	{
		/// <summary>Specifies that the text direction is inherited from the parent control.</summary>
		// Token: 0x04001B82 RID: 7042
		Inherit,
		/// <summary>Specifies horizontal text orientation.</summary>
		// Token: 0x04001B83 RID: 7043
		Horizontal,
		/// <summary>Specifies that text is to be rotated 90 degrees.</summary>
		// Token: 0x04001B84 RID: 7044
		Vertical90,
		/// <summary>Specifies that text is to be rotated 270 degrees.</summary>
		// Token: 0x04001B85 RID: 7045
		Vertical270
	}
}
