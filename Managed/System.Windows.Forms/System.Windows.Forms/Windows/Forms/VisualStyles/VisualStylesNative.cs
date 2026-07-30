using System;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200062D RID: 1581
	internal class VisualStylesNative : IVisualStyles
	{
		// Token: 0x06005038 RID: 20536 RVA: 0x001395DC File Offset: 0x001377DC
		public int UxThemeCloseThemeData(IntPtr hTheme)
		{
			return UXTheme.CloseThemeData(hTheme);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x001395E4 File Offset: 0x001377E4
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			int num = UXTheme.DrawThemeBackground(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, IntPtr.Zero);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00139618 File Offset: 0x00137818
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2 = XplatUIWin32.RECT.FromRectangle(clipRectangle);
			int num = UXTheme.DrawThemeBackground(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, ref rect2);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00139650 File Offset: 0x00137850
		public int UxThemeDrawThemeEdge(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects, out Rectangle result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2;
			int num = UXTheme.DrawThemeEdge(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, (uint)style, (uint)(edges + (int)effects), out rect2);
			dc.ReleaseHdc();
			result = rect2.ToRectangle();
			return num;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00139698 File Offset: 0x00137898
		public int UxThemeDrawThemeParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			int num;
			using (Graphics graphics = Graphics.FromHwnd(childControl.Handle))
			{
				IntPtr hdc = graphics.GetHdc();
				num = UXTheme.DrawThemeParentBackground(childControl.Handle, hdc, ref rect);
				graphics.ReleaseHdc(hdc);
			}
			return num;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00139704 File Offset: 0x00137904
		public int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			int num = UXTheme.DrawThemeText(hTheme, dc.GetHdc(), iPartId, iStateId, text, text.Length, (uint)textFlags, 0U, ref rect);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00139740 File Offset: 0x00137940
		public int UxThemeGetThemeBackgroundContentRect(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Rectangle result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2;
			int themeBackgroundContentRect = UXTheme.GetThemeBackgroundContentRect(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, out rect2);
			dc.ReleaseHdc();
			result = rect2.ToRectangle();
			return themeBackgroundContentRect;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00139780 File Offset: 0x00137980
		public int UxThemeGetThemeBackgroundExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle contentBounds, out Rectangle result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(contentBounds);
			XplatUIWin32.RECT rect2 = default(XplatUIWin32.RECT);
			int themeBackgroundExtent = UXTheme.GetThemeBackgroundExtent(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, ref rect2);
			dc.ReleaseHdc();
			result = rect2.ToRectangle();
			return themeBackgroundExtent;
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x001397C8 File Offset: 0x001379C8
		public int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			IntPtr intPtr;
			int themeBackgroundRegion = UXTheme.GetThemeBackgroundRegion(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, out intPtr);
			dc.ReleaseHdc();
			result = Region.FromHrgn(intPtr);
			return themeBackgroundRegion;
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00139804 File Offset: 0x00137A04
		public int UxThemeGetThemeBool(IntPtr hTheme, int iPartId, int iStateId, BooleanProperty prop, out bool result)
		{
			int num;
			int themeBool = UXTheme.GetThemeBool(hTheme, iPartId, iStateId, (int)prop, out num);
			result = num != 0;
			return themeBool;
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x00139830 File Offset: 0x00137A30
		public int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result)
		{
			int num;
			int themeColor = UXTheme.GetThemeColor(hTheme, iPartId, iStateId, (int)prop, out num);
			result = Color.FromArgb((int)(255L & (long)num), (int)(65280L & (long)num) >> 8, (int)(16711680L & (long)num) >> 16);
			return themeColor;
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x0013987C File Offset: 0x00137A7C
		public int UxThemeGetThemeEnumValue(IntPtr hTheme, int iPartId, int iStateId, EnumProperty prop, out int result)
		{
			int num;
			int themeEnumValue = UXTheme.GetThemeEnumValue(hTheme, iPartId, iStateId, (int)prop, out num);
			result = num;
			return themeEnumValue;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0013989C File Offset: 0x00137A9C
		public int UxThemeGetThemeFilename(IntPtr hTheme, int iPartId, int iStateId, FilenameProperty prop, out string result)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			int themeFilename = UXTheme.GetThemeFilename(hTheme, iPartId, iStateId, (int)prop, stringBuilder, stringBuilder.Capacity);
			result = stringBuilder.ToString();
			return themeFilename;
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x001398D0 File Offset: 0x00137AD0
		public int UxThemeGetThemeInt(IntPtr hTheme, int iPartId, int iStateId, IntegerProperty prop, out int result)
		{
			int num;
			int themeInt = UXTheme.GetThemeInt(hTheme, iPartId, iStateId, (int)prop, out num);
			result = num;
			return themeInt;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x001398F0 File Offset: 0x00137AF0
		public int UxThemeGetThemeMargins(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, MarginProperty prop, out Padding result)
		{
			UXTheme.MARGINS margins = default(UXTheme.MARGINS);
			XplatUIWin32.RECT rect;
			int themeMargins = UXTheme.GetThemeMargins(hTheme, dc.GetHdc(), iPartId, iStateId, (int)prop, out rect, out margins);
			dc.ReleaseHdc();
			result = margins.ToPadding();
			return themeMargins;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00139930 File Offset: 0x00137B30
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, ThemeSizeType type, out Size result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			UXTheme.SIZE size;
			int themePartSize = UXTheme.GetThemePartSize(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, (int)type, out size);
			dc.ReleaseHdc();
			result = size.ToSize();
			return themePartSize;
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00139970 File Offset: 0x00137B70
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result)
		{
			UXTheme.SIZE size;
			int themePartSize = UXTheme.GetThemePartSize(hTheme, dc.GetHdc(), iPartId, iStateId, IntPtr.Zero, (int)type, out size);
			dc.ReleaseHdc();
			result = size.ToSize();
			return themePartSize;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x001399AC File Offset: 0x00137BAC
		public int UxThemeGetThemePosition(IntPtr hTheme, int iPartId, int iStateId, PointProperty prop, out Point result)
		{
			POINT point;
			int themePosition = UXTheme.GetThemePosition(hTheme, iPartId, iStateId, (int)prop, out point);
			result = point.ToPoint();
			return themePosition;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x001399D8 File Offset: 0x00137BD8
		public int UxThemeGetThemeString(IntPtr hTheme, int iPartId, int iStateId, StringProperty prop, out string result)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			int themeString = UXTheme.GetThemeString(hTheme, iPartId, iStateId, (int)prop, stringBuilder, stringBuilder.Capacity);
			result = stringBuilder.ToString();
			return themeString;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x00139A0C File Offset: 0x00137C0C
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2;
			int themeTextExtent = UXTheme.GetThemeTextExtent(hTheme, dc.GetHdc(), iPartId, iStateId, textToDraw, textToDraw.Length, (int)flags, ref rect, out rect2);
			dc.ReleaseHdc();
			result = rect2.ToRectangle();
			return themeTextExtent;
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x00139A58 File Offset: 0x00137C58
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, out Rectangle result)
		{
			XplatUIWin32.RECT rect;
			int themeTextExtent = UXTheme.GetThemeTextExtent(hTheme, dc.GetHdc(), iPartId, iStateId, textToDraw, textToDraw.Length, (int)flags, 0, out rect);
			dc.ReleaseHdc();
			result = rect.ToRectangle();
			return themeTextExtent;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00139A98 File Offset: 0x00137C98
		public int UxThemeGetThemeTextMetrics(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, out TextMetrics result)
		{
			XplatUIWin32.TEXTMETRIC textmetric;
			int themeTextMetrics = UXTheme.GetThemeTextMetrics(hTheme, dc.GetHdc(), iPartId, iStateId, out textmetric);
			dc.ReleaseHdc();
			result = new TextMetrics
			{
				Ascent = textmetric.tmAscent,
				AverageCharWidth = textmetric.tmAveCharWidth,
				BreakChar = (char)textmetric.tmBreakChar,
				CharSet = (TextMetricsCharacterSet)textmetric.tmCharSet,
				DefaultChar = (char)textmetric.tmDefaultChar,
				Descent = textmetric.tmDescent,
				DigitizedAspectX = textmetric.tmDigitizedAspectX,
				DigitizedAspectY = textmetric.tmDigitizedAspectY,
				ExternalLeading = textmetric.tmExternalLeading,
				FirstChar = (char)textmetric.tmFirstChar,
				Height = textmetric.tmHeight,
				InternalLeading = textmetric.tmInternalLeading,
				Italic = (textmetric.tmItalic != 0),
				LastChar = (char)textmetric.tmLastChar,
				MaxCharWidth = textmetric.tmMaxCharWidth,
				Overhang = textmetric.tmOverhang,
				PitchAndFamily = (TextMetricsPitchAndFamilyValues)textmetric.tmPitchAndFamily,
				StruckOut = (textmetric.tmStruckOut != 0),
				Underlined = (textmetric.tmUnderlined != 0),
				Weight = textmetric.tmWeight
			};
			return themeTextMetrics;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00139C10 File Offset: 0x00137E10
		public int UxThemeHitTestThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, HitTestOptions options, Rectangle backgroundRectangle, IntPtr hrgn, Point pt, out HitTestCode result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(backgroundRectangle);
			int num2;
			int num = UXTheme.HitTestThemeBackground(hTheme, dc.GetHdc(), iPartId, iStateId, (uint)options, ref rect, hrgn, new POINT(pt.X, pt.Y), out num2);
			dc.ReleaseHdc();
			result = (HitTestCode)num2;
			return num;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00139C5C File Offset: 0x00137E5C
		public bool UxThemeIsAppThemed()
		{
			return UXTheme.IsAppThemed();
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00139C64 File Offset: 0x00137E64
		public bool UxThemeIsThemeActive()
		{
			return UXTheme.IsThemeActive();
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x00139C6C File Offset: 0x00137E6C
		public bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId)
		{
			return UXTheme.IsThemePartDefined(hTheme, iPartId, 0);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00139C78 File Offset: 0x00137E78
		public bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId)
		{
			return UXTheme.IsThemeBackgroundPartiallyTransparent(hTheme, iPartId, iStateId) != 0;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x00139C9C File Offset: 0x00137E9C
		public IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList)
		{
			return UXTheme.OpenThemeData(hWnd, classList);
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x00139CA8 File Offset: 0x00137EA8
		public string VisualStyleInformationAuthor
		{
			get
			{
				return VisualStylesNative.GetData("AUTHOR");
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06005055 RID: 20565 RVA: 0x00139CB4 File Offset: 0x00137EB4
		public string VisualStyleInformationColorScheme
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				StringBuilder stringBuilder2 = new StringBuilder(260);
				StringBuilder stringBuilder3 = new StringBuilder(260);
				UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
				return stringBuilder2.ToString();
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x00139D04 File Offset: 0x00137F04
		public string VisualStyleInformationCompany
		{
			get
			{
				return VisualStylesNative.GetData("COMPANY");
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06005057 RID: 20567 RVA: 0x00139D10 File Offset: 0x00137F10
		public Color VisualStyleInformationControlHighlightHot
		{
			get
			{
				IntPtr intPtr = UXTheme.OpenThemeData(IntPtr.Zero, "BUTTON");
				uint themeSysColor = UXTheme.GetThemeSysColor(intPtr, 1621);
				UXTheme.CloseThemeData(intPtr);
				return Color.FromArgb((int)(255U & themeSysColor), (int)(65280U & themeSysColor) >> 8, (int)(16711680U & themeSysColor) >> 16);
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x00139D60 File Offset: 0x00137F60
		public string VisualStyleInformationCopyright
		{
			get
			{
				return VisualStylesNative.GetData("COPYRIGHT");
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x00139D6C File Offset: 0x00137F6C
		public string VisualStyleInformationDescription
		{
			get
			{
				return VisualStylesNative.GetData("DESCRIPTION");
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x00139D78 File Offset: 0x00137F78
		public string VisualStyleInformationDisplayName
		{
			get
			{
				return VisualStylesNative.GetData("DISPLAYNAME");
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x00139D84 File Offset: 0x00137F84
		public string VisualStyleInformationFileName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				StringBuilder stringBuilder2 = new StringBuilder(260);
				StringBuilder stringBuilder3 = new StringBuilder(260);
				UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00139DD4 File Offset: 0x00137FD4
		private static string GetData(string propertyName)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			StringBuilder stringBuilder2 = new StringBuilder(260);
			StringBuilder stringBuilder3 = new StringBuilder(260);
			UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
			StringBuilder stringBuilder4 = new StringBuilder(260);
			UXTheme.GetThemeDocumentationProperty(stringBuilder.ToString(), propertyName, stringBuilder4, stringBuilder4.Capacity);
			return stringBuilder4.ToString();
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x00139E44 File Offset: 0x00138044
		public bool VisualStyleInformationIsSupportedByOS
		{
			get
			{
				return VisualStylesNative.IsSupported();
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600505E RID: 20574 RVA: 0x00139E4C File Offset: 0x0013804C
		public int VisualStyleInformationMinimumColorDepth
		{
			get
			{
				IntPtr intPtr = UXTheme.OpenThemeData(IntPtr.Zero, "BUTTON");
				int num;
				UXTheme.GetThemeSysInt(intPtr, 1301, out num);
				UXTheme.CloseThemeData(intPtr);
				return num;
			}
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x00139E80 File Offset: 0x00138080
		public static bool IsSupported()
		{
			return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version >= new Version(5, 1);
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x00139EBC File Offset: 0x001380BC
		public string VisualStyleInformationSize
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				StringBuilder stringBuilder2 = new StringBuilder(260);
				StringBuilder stringBuilder3 = new StringBuilder(260);
				UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
				return stringBuilder3.ToString();
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x00139F0C File Offset: 0x0013810C
		public bool VisualStyleInformationSupportsFlatMenus
		{
			get
			{
				IntPtr intPtr = UXTheme.OpenThemeData(IntPtr.Zero, "BUTTON");
				bool themeSysBool = UXTheme.GetThemeSysBool(intPtr, 1001) != 0;
				UXTheme.CloseThemeData(intPtr);
				return themeSysBool;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005062 RID: 20578 RVA: 0x00139F4C File Offset: 0x0013814C
		public Color VisualStyleInformationTextControlBorder
		{
			get
			{
				IntPtr intPtr = UXTheme.OpenThemeData(IntPtr.Zero, "EDIT");
				uint themeSysColor = UXTheme.GetThemeSysColor(intPtr, 1611);
				UXTheme.CloseThemeData(intPtr);
				return Color.FromArgb((int)(255U & themeSysColor), (int)(65280U & themeSysColor) >> 8, (int)(16711680U & themeSysColor) >> 16);
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x00139F9C File Offset: 0x0013819C
		public string VisualStyleInformationUrl
		{
			get
			{
				return VisualStylesNative.GetData("URL");
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06005064 RID: 20580 RVA: 0x00139FA8 File Offset: 0x001381A8
		public string VisualStyleInformationVersion
		{
			get
			{
				return VisualStylesNative.GetData("VERSION");
			}
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00139FB4 File Offset: 0x001381B4
		public void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			IntPtr hdc = dc.GetHdc();
			XplatUIWin32.Win32ExcludeClipRect(hdc, excludedArea.Left, excludedArea.Top, excludedArea.Right, excludedArea.Bottom);
			UXTheme.DrawThemeBackground(theme, hdc, part, state, ref rect, IntPtr.Zero);
			IntPtr intPtr = XplatUIWin32.Win32CreateRectRgn(excludedArea.Left, excludedArea.Top, excludedArea.Right, excludedArea.Bottom);
			XplatUIWin32.Win32ExtSelectClipRgn(hdc, intPtr, 2);
			XplatUIWin32.Win32DeleteObject(intPtr);
			dc.ReleaseHdc();
		}
	}
}
