using System;

namespace System.Windows.Forms
{
	/// <summary>Determines the alignment of a <see cref="T:System.Windows.Forms.ToolStripItem" /> in a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000357 RID: 855
	public enum ToolStripItemAlignment
	{
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be anchored toward the left or top end of the <see cref="T:System.Windows.Forms.ToolStrip" />, depending on the <see cref="T:System.Windows.Forms.ToolStrip" /> orientation. If the value of <see cref="T:System.Windows.Forms.RightToLeft" /> is Yes, items marked as <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Left" /> are aligned to the right side of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001AE1 RID: 6881
		Left,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be anchored toward the right or bottom end of the <see cref="T:System.Windows.Forms.ToolStrip" />, depending on the <see cref="T:System.Windows.Forms.ToolStrip" /> orientation. If the value of <see cref="T:System.Windows.Forms.RightToLeft" /> is Yes, items marked as <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Right" /> are aligned to the left side of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001AE2 RID: 6882
		Right
	}
}
