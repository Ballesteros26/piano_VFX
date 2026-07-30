using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200009E RID: 158
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct LOGFONT
	{
		// Token: 0x040005E6 RID: 1510
		internal int lfHeight;

		// Token: 0x040005E7 RID: 1511
		internal uint lfWidth;

		// Token: 0x040005E8 RID: 1512
		internal uint lfEscapement;

		// Token: 0x040005E9 RID: 1513
		internal uint lfOrientation;

		// Token: 0x040005EA RID: 1514
		internal uint lfWeight;

		// Token: 0x040005EB RID: 1515
		internal byte lfItalic;

		// Token: 0x040005EC RID: 1516
		internal byte lfUnderline;

		// Token: 0x040005ED RID: 1517
		internal byte lfStrikeOut;

		// Token: 0x040005EE RID: 1518
		internal byte lfCharSet;

		// Token: 0x040005EF RID: 1519
		internal byte lfOutPrecision;

		// Token: 0x040005F0 RID: 1520
		internal byte lfClipPrecision;

		// Token: 0x040005F1 RID: 1521
		internal byte lfQuality;

		// Token: 0x040005F2 RID: 1522
		internal byte lfPitchAndFamily;

		// Token: 0x040005F3 RID: 1523
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string lfFaceName;
	}
}
