using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential)]
	internal class MetafileHeaderEmf
	{
		// Token: 0x040009CD RID: 2509
		public MetafileType type;

		// Token: 0x040009CE RID: 2510
		public int size;

		// Token: 0x040009CF RID: 2511
		public int version;

		// Token: 0x040009D0 RID: 2512
		public EmfPlusFlags emfPlusFlags;

		// Token: 0x040009D1 RID: 2513
		public float dpiX;

		// Token: 0x040009D2 RID: 2514
		public float dpiY;

		// Token: 0x040009D3 RID: 2515
		public int X;

		// Token: 0x040009D4 RID: 2516
		public int Y;

		// Token: 0x040009D5 RID: 2517
		public int Width;

		// Token: 0x040009D6 RID: 2518
		public int Height;

		// Token: 0x040009D7 RID: 2519
		public SafeNativeMethods.ENHMETAHEADER EmfHeader;

		// Token: 0x040009D8 RID: 2520
		public int EmfPlusHeaderSize;

		// Token: 0x040009D9 RID: 2521
		public int LogicalDpiX;

		// Token: 0x040009DA RID: 2522
		public int LogicalDpiY;
	}
}
