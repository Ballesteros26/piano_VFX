using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to its parent control.</summary>
	// Token: 0x0200034D RID: 845
	public enum ToolStripDropDownDirection
	{
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the left of its parent control.</summary>
		// Token: 0x04001A79 RID: 6777
		AboveLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the right of its parent control.</summary>
		// Token: 0x04001A7A RID: 6778
		AboveRight,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the left of its parent control.</summary>
		// Token: 0x04001A7B RID: 6779
		BelowLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the right of its parent control.</summary>
		// Token: 0x04001A7C RID: 6780
		BelowRight,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the left of its parent control.</summary>
		// Token: 0x04001A7D RID: 6781
		Left,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the right of its parent control.</summary>
		// Token: 0x04001A7E RID: 6782
		Right,
		/// <summary>Compensates for nested drop-down controls and responds to the <see cref="T:System.Windows.Forms.RightToLeft" /> setting, specifying either <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Left" /> or <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Right" /> accordingly.</summary>
		// Token: 0x04001A7F RID: 6783
		Default = 7
	}
}
