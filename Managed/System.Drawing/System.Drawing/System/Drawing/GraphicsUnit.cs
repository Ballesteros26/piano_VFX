using System;

namespace System.Drawing
{
	/// <summary>Specifies the unit of measure for the given data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000068 RID: 104
	public enum GraphicsUnit
	{
		/// <summary>Specifies the world coordinate system unit as the unit of measure.</summary>
		// Token: 0x040003C4 RID: 964
		World,
		/// <summary>Specifies the unit of measure of the display device. Typically pixels for video displays, and 1/100 inch for printers.</summary>
		// Token: 0x040003C5 RID: 965
		Display,
		/// <summary>Specifies a device pixel as the unit of measure.</summary>
		// Token: 0x040003C6 RID: 966
		Pixel,
		/// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
		// Token: 0x040003C7 RID: 967
		Point,
		/// <summary>Specifies the inch as the unit of measure.</summary>
		// Token: 0x040003C8 RID: 968
		Inch,
		/// <summary>Specifies the document unit (1/300 inch) as the unit of measure.</summary>
		// Token: 0x040003C9 RID: 969
		Document,
		/// <summary>Specifies the millimeter as the unit of measure.</summary>
		// Token: 0x040003CA RID: 970
		Millimeter
	}
}
