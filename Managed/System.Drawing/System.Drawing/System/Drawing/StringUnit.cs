using System;

namespace System.Drawing
{
	/// <summary>Specifies the units of measure for a text string.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000037 RID: 55
	public enum StringUnit
	{
		/// <summary>Specifies world units as the unit of measure.</summary>
		// Token: 0x040002B1 RID: 689
		World,
		/// <summary>Specifies the device unit as the unit of measure.</summary>
		// Token: 0x040002B2 RID: 690
		Display,
		/// <summary>Specifies a pixel as the unit of measure.</summary>
		// Token: 0x040002B3 RID: 691
		Pixel,
		/// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
		// Token: 0x040002B4 RID: 692
		Point,
		/// <summary>Specifies an inch as the unit of measure.</summary>
		// Token: 0x040002B5 RID: 693
		Inch,
		/// <summary>Specifies 1/300 of an inch as the unit of measure.</summary>
		// Token: 0x040002B6 RID: 694
		Document,
		/// <summary>Specifies a millimeter as the unit of measure </summary>
		// Token: 0x040002B7 RID: 695
		Millimeter,
		/// <summary>Specifies a printer's em size of 32 as the unit of measure.</summary>
		// Token: 0x040002B8 RID: 696
		Em = 32
	}
}
