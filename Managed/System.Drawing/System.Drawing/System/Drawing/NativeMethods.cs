using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200002B RID: 43
	internal class NativeMethods
	{
		// Token: 0x04000247 RID: 583
		internal static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x04000248 RID: 584
		public const int MAX_PATH = 260;

		// Token: 0x04000249 RID: 585
		internal const int SM_REMOTESESSION = 4096;

		// Token: 0x0400024A RID: 586
		internal const int OBJ_DC = 3;

		// Token: 0x0400024B RID: 587
		internal const int OBJ_METADC = 4;

		// Token: 0x0400024C RID: 588
		internal const int OBJ_MEMDC = 10;

		// Token: 0x0400024D RID: 589
		internal const int OBJ_ENHMETADC = 12;

		// Token: 0x0400024E RID: 590
		internal const int DIB_RGB_COLORS = 0;

		// Token: 0x0400024F RID: 591
		internal const int BI_BITFIELDS = 3;

		// Token: 0x04000250 RID: 592
		internal const int BI_RGB = 0;

		// Token: 0x04000251 RID: 593
		internal const int BITMAPINFO_MAX_COLORSIZE = 256;

		// Token: 0x04000252 RID: 594
		internal const int SPI_GETICONTITLELOGFONT = 31;

		// Token: 0x04000253 RID: 595
		internal const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x04000254 RID: 596
		internal const int DEFAULT_GUI_FONT = 17;

		// Token: 0x0200002C RID: 44
		internal struct BITMAPINFO_FLAT
		{
			// Token: 0x04000255 RID: 597
			public int bmiHeader_biSize;

			// Token: 0x04000256 RID: 598
			public int bmiHeader_biWidth;

			// Token: 0x04000257 RID: 599
			public int bmiHeader_biHeight;

			// Token: 0x04000258 RID: 600
			public short bmiHeader_biPlanes;

			// Token: 0x04000259 RID: 601
			public short bmiHeader_biBitCount;

			// Token: 0x0400025A RID: 602
			public int bmiHeader_biCompression;

			// Token: 0x0400025B RID: 603
			public int bmiHeader_biSizeImage;

			// Token: 0x0400025C RID: 604
			public int bmiHeader_biXPelsPerMeter;

			// Token: 0x0400025D RID: 605
			public int bmiHeader_biYPelsPerMeter;

			// Token: 0x0400025E RID: 606
			public int bmiHeader_biClrUsed;

			// Token: 0x0400025F RID: 607
			public int bmiHeader_biClrImportant;

			// Token: 0x04000260 RID: 608
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public byte[] bmiColors;
		}

		// Token: 0x0200002D RID: 45
		[StructLayout(LayoutKind.Sequential)]
		internal class BITMAPINFOHEADER
		{
			// Token: 0x04000261 RID: 609
			public int biSize = 40;

			// Token: 0x04000262 RID: 610
			public int biWidth;

			// Token: 0x04000263 RID: 611
			public int biHeight;

			// Token: 0x04000264 RID: 612
			public short biPlanes;

			// Token: 0x04000265 RID: 613
			public short biBitCount;

			// Token: 0x04000266 RID: 614
			public int biCompression;

			// Token: 0x04000267 RID: 615
			public int biSizeImage;

			// Token: 0x04000268 RID: 616
			public int biXPelsPerMeter;

			// Token: 0x04000269 RID: 617
			public int biYPelsPerMeter;

			// Token: 0x0400026A RID: 618
			public int biClrUsed;

			// Token: 0x0400026B RID: 619
			public int biClrImportant;
		}

		// Token: 0x0200002E RID: 46
		internal struct PALETTEENTRY
		{
			// Token: 0x0400026C RID: 620
			public byte peRed;

			// Token: 0x0400026D RID: 621
			public byte peGreen;

			// Token: 0x0400026E RID: 622
			public byte peBlue;

			// Token: 0x0400026F RID: 623
			public byte peFlags;
		}

		// Token: 0x0200002F RID: 47
		internal struct RGBQUAD
		{
			// Token: 0x04000270 RID: 624
			public byte rgbBlue;

			// Token: 0x04000271 RID: 625
			public byte rgbGreen;

			// Token: 0x04000272 RID: 626
			public byte rgbRed;

			// Token: 0x04000273 RID: 627
			public byte rgbReserved;
		}

		// Token: 0x02000030 RID: 48
		[StructLayout(LayoutKind.Sequential)]
		internal class NONCLIENTMETRICS
		{
			// Token: 0x04000274 RID: 628
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NONCLIENTMETRICS));

			// Token: 0x04000275 RID: 629
			public int iBorderWidth;

			// Token: 0x04000276 RID: 630
			public int iScrollWidth;

			// Token: 0x04000277 RID: 631
			public int iScrollHeight;

			// Token: 0x04000278 RID: 632
			public int iCaptionWidth;

			// Token: 0x04000279 RID: 633
			public int iCaptionHeight;

			// Token: 0x0400027A RID: 634
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfCaptionFont;

			// Token: 0x0400027B RID: 635
			public int iSmCaptionWidth;

			// Token: 0x0400027C RID: 636
			public int iSmCaptionHeight;

			// Token: 0x0400027D RID: 637
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfSmCaptionFont;

			// Token: 0x0400027E RID: 638
			public int iMenuWidth;

			// Token: 0x0400027F RID: 639
			public int iMenuHeight;

			// Token: 0x04000280 RID: 640
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfMenuFont;

			// Token: 0x04000281 RID: 641
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfStatusFont;

			// Token: 0x04000282 RID: 642
			[MarshalAs(UnmanagedType.Struct)]
			public SafeNativeMethods.LOGFONT lfMessageFont;
		}
	}
}
