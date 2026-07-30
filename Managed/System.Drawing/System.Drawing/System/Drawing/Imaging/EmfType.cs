using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the nature of the records that are placed in an Enhanced Metafile (EMF) file. This enumeration is used by several constructors in the <see cref="T:System.Drawing.Imaging.Metafile" /> class.</summary>
	// Token: 0x020000FC RID: 252
	public enum EmfType
	{
		/// <summary>Specifies that all the records in the metafile are EMF records, which can be displayed by GDI or GDI+.</summary>
		// Token: 0x04000956 RID: 2390
		EmfOnly = 3,
		/// <summary>Specifies that all the records in the metafile are EMF+ records, which can be displayed by GDI+ but not by GDI.</summary>
		// Token: 0x04000957 RID: 2391
		EmfPlusOnly,
		/// <summary>Specifies that all EMF+ records in the metafile are associated with an alternate EMF record. Metafiles of type <see cref="F:System.Drawing.Imaging.EmfType.EmfPlusDual" /> can be displayed by GDI or by GDI+.</summary>
		// Token: 0x04000958 RID: 2392
		EmfPlusDual
	}
}
