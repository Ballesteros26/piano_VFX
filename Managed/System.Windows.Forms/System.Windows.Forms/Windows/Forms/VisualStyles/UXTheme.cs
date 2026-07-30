using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000534 RID: 1332
	internal class UXTheme
	{
		// Token: 0x06004DCF RID: 19919
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int CloseThemeData(IntPtr hTheme);

		// Token: 0x06004DD0 RID: 19920
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, ref XplatUIWin32.RECT pClipRect);

		// Token: 0x06004DD1 RID: 19921
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, IntPtr pClipRect);

		// Token: 0x06004DD2 RID: 19922
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pDestRect, uint egde, uint flags, out XplatUIWin32.RECT pRect);

		// Token: 0x06004DD3 RID: 19923
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pDestRect, uint edge, uint flags, int pRect);

		// Token: 0x06004DD4 RID: 19924
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeIcon(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, IntPtr himl, int iImageIndex);

		// Token: 0x06004DD5 RID: 19925
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, ref XplatUIWin32.RECT pRect);

		// Token: 0x06004DD6 RID: 19926
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, int pRect);

		// Token: 0x06004DD7 RID: 19927
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int textLength, uint textFlags, uint textFlags2, ref XplatUIWin32.RECT pRect);

		// Token: 0x06004DD8 RID: 19928
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int EnableTheming(int fEnable);

		// Token: 0x06004DD9 RID: 19929
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

		// Token: 0x06004DDA RID: 19930
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeBackgroundContentRect(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pBoundingRect, out XplatUIWin32.RECT pContentRect);

		// Token: 0x06004DDB RID: 19931
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeBackgroundExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, ref XplatUIWin32.RECT pClipRect);

		// Token: 0x06004DDC RID: 19932
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeBackgroundRegion(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, out IntPtr pRegion);

		// Token: 0x06004DDD RID: 19933
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeBool(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int pfVal);

		// Token: 0x06004DDE RID: 19934
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int pColor);

		// Token: 0x06004DDF RID: 19935
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeEnumValue(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int piVal);

		// Token: 0x06004DE0 RID: 19936
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeFilename(IntPtr hTheme, int iPartId, int iStateId, int iPropId, [MarshalAs(21)] StringBuilder themeFileName, int themeFileNameLength);

		// Token: 0x06004DE1 RID: 19937
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeFont(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, [MarshalAs(43)] out UXTheme.LOGFONT lf);

		// Token: 0x06004DE2 RID: 19938
		[DllImport("gdi32", CharSet = 4)]
		public static extern IntPtr CreateFontIndirect([MarshalAs(43)] [In] UXTheme.LOGFONT lplf);

		// Token: 0x06004DE3 RID: 19939
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeInt(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int piVal);

		// Token: 0x06004DE4 RID: 19940
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, out XplatUIWin32.RECT prc, out UXTheme.MARGINS pMargins);

		// Token: 0x06004DE5 RID: 19941
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, int eSize, out UXTheme.SIZE size);

		// Token: 0x06004DE6 RID: 19942
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, IntPtr pRect, int eSize, out UXTheme.SIZE size);

		// Token: 0x06004DE7 RID: 19943
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemePosition(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out POINT pPoint);

		// Token: 0x06004DE8 RID: 19944
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeString(IntPtr hTheme, int iPartId, int iStateId, int iPropId, [MarshalAs(21)] StringBuilder themeString, int themeStringLength);

		// Token: 0x06004DE9 RID: 19945
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeTextExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int textLength, int textFlags, ref XplatUIWin32.RECT boundingRect, out XplatUIWin32.RECT extentRect);

		// Token: 0x06004DEA RID: 19946
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeTextExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int textLength, int textFlags, int boundingRect, out XplatUIWin32.RECT extentRect);

		// Token: 0x06004DEB RID: 19947
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeTextMetrics(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, out XplatUIWin32.TEXTMETRIC textMetric);

		// Token: 0x06004DEC RID: 19948
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int HitTestThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, uint dwOptions, ref XplatUIWin32.RECT pRect, IntPtr hrgn, POINT ptTest, out HitTestCode code);

		// Token: 0x06004DED RID: 19949
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int IsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06004DEE RID: 19950
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern bool IsThemePartDefined(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06004DEF RID: 19951
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern bool IsThemeActive();

		// Token: 0x06004DF0 RID: 19952
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern bool IsAppThemed();

		// Token: 0x06004DF1 RID: 19953
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int HitTestThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, uint dwOptions, ref XplatUIWin32.RECT pRect, IntPtr hrgn, POINT ptTest, out int code);

		// Token: 0x06004DF2 RID: 19954
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeDocumentationProperty(string stringThemeName, string stringPropertyName, StringBuilder stringValue, int lengthValue);

		// Token: 0x06004DF3 RID: 19955
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);

		// Token: 0x06004DF4 RID: 19956
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern uint GetThemeSysColor(IntPtr hTheme, int iColorId);

		// Token: 0x06004DF5 RID: 19957
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeSysInt(IntPtr hTheme, int iIntId, out int piVal);

		// Token: 0x06004DF6 RID: 19958
		[DllImport("uxtheme", CharSet = 3, ExactSpelling = true)]
		public static extern int GetThemeSysBool(IntPtr hTheme, int iBoolId);

		// Token: 0x02000535 RID: 1333
		[StructLayout(0, CharSet = 4)]
		public class LOGFONT
		{
			// Token: 0x04002C27 RID: 11303
			public int lfHeight;

			// Token: 0x04002C28 RID: 11304
			public int lfWidth;

			// Token: 0x04002C29 RID: 11305
			public int lfEscapement;

			// Token: 0x04002C2A RID: 11306
			public int lfOrientation;

			// Token: 0x04002C2B RID: 11307
			public int lfWeight;

			// Token: 0x04002C2C RID: 11308
			public byte lfItalic;

			// Token: 0x04002C2D RID: 11309
			public byte lfUnderline;

			// Token: 0x04002C2E RID: 11310
			public byte lfStrikeOut;

			// Token: 0x04002C2F RID: 11311
			public byte lfCharSet;

			// Token: 0x04002C30 RID: 11312
			public byte lfOutPrecision;

			// Token: 0x04002C31 RID: 11313
			public byte lfClipPrecision;

			// Token: 0x04002C32 RID: 11314
			public byte lfQuality;

			// Token: 0x04002C33 RID: 11315
			public byte lfPitchAndFamily;

			// Token: 0x04002C34 RID: 11316
			[MarshalAs(23, SizeConst = 32)]
			public string lfFaceName = string.Empty;
		}

		// Token: 0x02000536 RID: 1334
		public struct MARGINS
		{
			// Token: 0x06004DF8 RID: 19960 RVA: 0x00135A40 File Offset: 0x00133C40
			public Padding ToPadding()
			{
				return new Padding(this.leftWidth, this.topHeight, this.rightWidth, this.bottomHeight);
			}

			// Token: 0x04002C35 RID: 11317
			public int leftWidth;

			// Token: 0x04002C36 RID: 11318
			public int rightWidth;

			// Token: 0x04002C37 RID: 11319
			public int topHeight;

			// Token: 0x04002C38 RID: 11320
			public int bottomHeight;
		}

		// Token: 0x02000537 RID: 1335
		public struct SIZE
		{
			// Token: 0x06004DF9 RID: 19961 RVA: 0x00135A60 File Offset: 0x00133C60
			public Size ToSize()
			{
				return new Size(this.cx, this.cy);
			}

			// Token: 0x04002C39 RID: 11321
			public int cx;

			// Token: 0x04002C3A RID: 11322
			public int cy;
		}
	}
}
