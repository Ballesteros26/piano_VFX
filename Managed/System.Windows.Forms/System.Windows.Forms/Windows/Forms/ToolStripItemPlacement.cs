using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies where a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000360 RID: 864
	public enum ToolStripItemPlacement
	{
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the main <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001AFF RID: 6911
		Main,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the overflow <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001B00 RID: 6912
		Overflow,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is not to be layed out on the screen.</summary>
		// Token: 0x04001B01 RID: 6913
		None
	}
}
