using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the unit of measurement.</summary>
	// Token: 0x02000323 RID: 803
	public enum UnitType
	{
		/// <summary>Measurement is in pixels.</summary>
		// Token: 0x040017B4 RID: 6068
		Pixel = 1,
		/// <summary>Measurement is in points. A point represents 1/72 of an inch.</summary>
		// Token: 0x040017B5 RID: 6069
		Point,
		/// <summary>Measurement is in picas. A pica represents 12 points.</summary>
		// Token: 0x040017B6 RID: 6070
		Pica,
		/// <summary>Measurement is in inches.</summary>
		// Token: 0x040017B7 RID: 6071
		Inch,
		/// <summary>Measurement is in millimeters.</summary>
		// Token: 0x040017B8 RID: 6072
		Mm,
		/// <summary>Measurement is in centimeters.</summary>
		// Token: 0x040017B9 RID: 6073
		Cm,
		/// <summary>Measurement is a percentage relative to the parent element.</summary>
		// Token: 0x040017BA RID: 6074
		Percentage,
		/// <summary>Measurement is relative to the height of the parent element's font.</summary>
		// Token: 0x040017BB RID: 6075
		Em,
		/// <summary>Measurement is relative to the height of the lowercase letter x of the parent element's font.</summary>
		// Token: 0x040017BC RID: 6076
		Ex
	}
}
