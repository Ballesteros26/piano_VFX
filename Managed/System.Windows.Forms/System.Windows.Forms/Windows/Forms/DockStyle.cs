using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position and manner in which a control is docked.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014B RID: 331
	[Editor("System.Windows.Forms.Design.DockEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum DockStyle
	{
		/// <summary>The control is not docked.</summary>
		// Token: 0x04000CA4 RID: 3236
		None,
		/// <summary>The control's top edge is docked to the top of its containing control.</summary>
		// Token: 0x04000CA5 RID: 3237
		Top,
		/// <summary>The control's bottom edge is docked to the bottom of its containing control.</summary>
		// Token: 0x04000CA6 RID: 3238
		Bottom,
		/// <summary>The control's left edge is docked to the left edge of its containing control.</summary>
		// Token: 0x04000CA7 RID: 3239
		Left,
		/// <summary>The control's right edge is docked to the right edge of its containing control.</summary>
		// Token: 0x04000CA8 RID: 3240
		Right,
		/// <summary>All the control's edges are docked to the all edges of its containing control and sized appropriately.</summary>
		// Token: 0x04000CA9 RID: 3241
		Fill
	}
}
