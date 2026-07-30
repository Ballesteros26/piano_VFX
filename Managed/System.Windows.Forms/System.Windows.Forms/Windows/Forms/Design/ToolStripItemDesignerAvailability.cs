using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies controls that are visible in the designer.</summary>
	// Token: 0x02000018 RID: 24
	[Flags]
	public enum ToolStripItemDesignerAvailability
	{
		/// <summary>Specifies that no controls are visible.</summary>
		// Token: 0x04000050 RID: 80
		None = 0,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ToolStrip" /> is visible.</summary>
		// Token: 0x04000051 RID: 81
		ToolStrip = 1,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.MenuStrip" /> is visible.</summary>
		// Token: 0x04000052 RID: 82
		MenuStrip = 2,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ContextMenuStrip" /> is visible.</summary>
		// Token: 0x04000053 RID: 83
		ContextMenuStrip = 4,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.StatusStrip" /> is visible.</summary>
		// Token: 0x04000054 RID: 84
		StatusStrip = 8,
		/// <summary>Specifies that all controls are visible.</summary>
		// Token: 0x04000055 RID: 85
		All = 15
	}
}
