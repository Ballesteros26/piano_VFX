using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" /> while retaining the original image proportions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200035E RID: 862
	public enum ToolStripItemImageScaling
	{
		/// <summary>Specifies that the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is not automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001AF8 RID: 6904
		None,
		/// <summary>Specifies that the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04001AF9 RID: 6905
		SizeToFit
	}
}
