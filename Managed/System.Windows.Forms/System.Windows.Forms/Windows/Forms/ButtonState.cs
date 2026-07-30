using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance of a button.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006E RID: 110
	[Flags]
	public enum ButtonState
	{
		/// <summary>The button has its normal appearance (three-dimensional).</summary>
		// Token: 0x040006A5 RID: 1701
		Normal = 0,
		/// <summary>The button is inactive (grayed).</summary>
		// Token: 0x040006A6 RID: 1702
		Inactive = 256,
		/// <summary>The button appears pressed.</summary>
		// Token: 0x040006A7 RID: 1703
		Pushed = 512,
		/// <summary>The button has a checked or latched appearance. Use this appearance to show that a toggle button has been pressed.</summary>
		// Token: 0x040006A8 RID: 1704
		Checked = 1024,
		/// <summary>The button has a flat, two-dimensional appearance.</summary>
		// Token: 0x040006A9 RID: 1705
		Flat = 16384,
		/// <summary>All flags except Normal are set.</summary>
		// Token: 0x040006AA RID: 1706
		All = 18176
	}
}
