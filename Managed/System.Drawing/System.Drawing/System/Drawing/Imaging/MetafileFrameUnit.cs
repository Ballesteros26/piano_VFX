using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the unit of measurement for the rectangle used to size and position a metafile. This is specified during the creation of the <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
	// Token: 0x02000109 RID: 265
	public enum MetafileFrameUnit
	{
		/// <summary>The unit of measurement is 1 pixel.</summary>
		// Token: 0x040009C7 RID: 2503
		Pixel = 2,
		/// <summary>The unit of measurement is 1 printer's point.</summary>
		// Token: 0x040009C8 RID: 2504
		Point,
		/// <summary>The unit of measurement is 1 inch.</summary>
		// Token: 0x040009C9 RID: 2505
		Inch,
		/// <summary>The unit of measurement is 1/300 of an inch.</summary>
		// Token: 0x040009CA RID: 2506
		Document,
		/// <summary>The unit of measurement is 1 millimeter.</summary>
		// Token: 0x040009CB RID: 2507
		Millimeter,
		/// <summary>The unit of measurement is 0.01 millimeter. Provided for compatibility with GDI.</summary>
		// Token: 0x040009CC RID: 2508
		GdiCompatible
	}
}
