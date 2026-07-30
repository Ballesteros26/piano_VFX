using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal class MetafileHeaderWmf
	{
		// Token: 0x040009DB RID: 2523
		public MetafileType type;

		// Token: 0x040009DC RID: 2524
		public int size = Marshal.SizeOf(typeof(MetafileHeaderWmf));

		// Token: 0x040009DD RID: 2525
		public int version;

		// Token: 0x040009DE RID: 2526
		public EmfPlusFlags emfPlusFlags;

		// Token: 0x040009DF RID: 2527
		public float dpiX;

		// Token: 0x040009E0 RID: 2528
		public float dpiY;

		// Token: 0x040009E1 RID: 2529
		public int X;

		// Token: 0x040009E2 RID: 2530
		public int Y;

		// Token: 0x040009E3 RID: 2531
		public int Width;

		// Token: 0x040009E4 RID: 2532
		public int Height;

		// Token: 0x040009E5 RID: 2533
		[MarshalAs(UnmanagedType.Struct)]
		public MetaHeader WmfHeader = new MetaHeader();

		// Token: 0x040009E6 RID: 2534
		public int dummy1;

		// Token: 0x040009E7 RID: 2535
		public int dummy2;

		// Token: 0x040009E8 RID: 2536
		public int dummy3;

		// Token: 0x040009E9 RID: 2537
		public int dummy4;

		// Token: 0x040009EA RID: 2538
		public int dummy5;

		// Token: 0x040009EB RID: 2539
		public int dummy6;

		// Token: 0x040009EC RID: 2540
		public int dummy7;

		// Token: 0x040009ED RID: 2541
		public int dummy8;

		// Token: 0x040009EE RID: 2542
		public int dummy9;

		// Token: 0x040009EF RID: 2543
		public int dummy10;

		// Token: 0x040009F0 RID: 2544
		public int dummy11;

		// Token: 0x040009F1 RID: 2545
		public int dummy12;

		// Token: 0x040009F2 RID: 2546
		public int dummy13;

		// Token: 0x040009F3 RID: 2547
		public int dummy14;

		// Token: 0x040009F4 RID: 2548
		public int dummy15;

		// Token: 0x040009F5 RID: 2549
		public int dummy16;

		// Token: 0x040009F6 RID: 2550
		public int EmfPlusHeaderSize;

		// Token: 0x040009F7 RID: 2551
		public int LogicalDpiX;

		// Token: 0x040009F8 RID: 2552
		public int LogicalDpiY;
	}
}
