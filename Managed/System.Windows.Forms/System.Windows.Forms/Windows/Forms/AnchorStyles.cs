using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control anchors to the edges of its container.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003D RID: 61
	[Flags]
	[Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum AnchorStyles
	{
		/// <summary>The control is not anchored to any edges of its container.</summary>
		// Token: 0x040005A4 RID: 1444
		None = 0,
		/// <summary>The control is anchored to the top edge of its container.</summary>
		// Token: 0x040005A5 RID: 1445
		Top = 1,
		/// <summary>The control is anchored to the bottom edge of its container.</summary>
		// Token: 0x040005A6 RID: 1446
		Bottom = 2,
		/// <summary>The control is anchored to the left edge of its container.</summary>
		// Token: 0x040005A7 RID: 1447
		Left = 4,
		/// <summary>The control is anchored to the right edge of its container.</summary>
		// Token: 0x040005A8 RID: 1448
		Right = 8
	}
}
