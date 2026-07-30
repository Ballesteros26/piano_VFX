using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies what to render (image or text) for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200035A RID: 858
	public enum ToolStripItemDisplayStyle
	{
		/// <summary>Specifies that neither image nor text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04001AE7 RID: 6887
		None,
		/// <summary>Specifies that only text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04001AE8 RID: 6888
		Text,
		/// <summary>Specifies that only an image is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04001AE9 RID: 6889
		Image,
		/// <summary>Specifies that both an image and text are to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04001AEA RID: 6890
		ImageAndText
	}
}
