using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the sides of a rectangle to apply a three-dimensional border to.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000065 RID: 101
	[Flags]
	[ComVisible(true)]
	public enum Border3DSide
	{
		/// <summary>A three-dimensional border on the left edge of the rectangle.</summary>
		// Token: 0x0400066B RID: 1643
		Left = 1,
		/// <summary>A three-dimensional border on the top edge of the rectangle.</summary>
		// Token: 0x0400066C RID: 1644
		Top = 2,
		/// <summary>A three-dimensional border on the right side of the rectangle.</summary>
		// Token: 0x0400066D RID: 1645
		Right = 4,
		/// <summary>A three-dimensional border on the bottom side of the rectangle.</summary>
		// Token: 0x0400066E RID: 1646
		Bottom = 8,
		/// <summary>The interior of the rectangle is filled with the color defined for three-dimensional controls instead of the background color for the form.</summary>
		// Token: 0x0400066F RID: 1647
		Middle = 2048,
		/// <summary>A three-dimensional border on all four sides of the rectangle. The middle of the rectangle is filled with the color defined for three-dimensional controls.</summary>
		// Token: 0x04000670 RID: 1648
		All = 2063
	}
}
