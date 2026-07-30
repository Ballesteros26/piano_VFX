using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Internal;
using System.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000018 RID: 24
	[SuppressUnmanagedCodeSecurity]
	internal class SafeNativeMethods
	{
		// Token: 0x06000053 RID: 83
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleBitmap", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);

		// Token: 0x06000054 RID: 84 RVA: 0x000029E3 File Offset: 0x00000BE3
		public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
		{
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateCompatibleBitmap(hDC, width, height), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06000055 RID: 85
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

		// Token: 0x06000056 RID: 86
		[DllImport("gdi32")]
		public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5);

		// Token: 0x06000057 RID: 87
		[DllImport("gdi32")]
		public static extern uint GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, byte[] lppe);

		// Token: 0x06000058 RID: 88
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateDIBSection", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);

		// Token: 0x06000059 RID: 89 RVA: 0x000029F7 File Offset: 0x00000BF7
		public static IntPtr CreateDIBSection(HandleRef hdc, ref NativeMethods.BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset)
		{
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateDIBSection(hdc, ref bmi, iUsage, ref ppvBits, hSection, dwOffset), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600005A RID: 90
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GlobalFree(HandleRef handle);

		// Token: 0x0600005B RID: 91
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StartDoc(HandleRef hDC, SafeNativeMethods.DOCINFO lpDocInfo);

		// Token: 0x0600005C RID: 92
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int StartPage(HandleRef hDC);

		// Token: 0x0600005D RID: 93
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int EndPage(HandleRef hDC);

		// Token: 0x0600005E RID: 94
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int AbortDoc(HandleRef hDC);

		// Token: 0x0600005F RID: 95
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int EndDoc(HandleRef hDC);

		// Token: 0x06000060 RID: 96
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PrintDlg([In] [Out] SafeNativeMethods.PRINTDLG lppd);

		// Token: 0x06000061 RID: 97
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PrintDlg([In] [Out] SafeNativeMethods.PRINTDLGX86 lppd);

		// Token: 0x06000062 RID: 98
		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DeviceCapabilities(string pDevice, string pPort, short fwCapabilities, IntPtr pOutput, IntPtr pDevMode);

		// Token: 0x06000063 RID: 99
		[DllImport("winspool.drv", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DocumentProperties(HandleRef hwnd, HandleRef hPrinter, string pDeviceName, IntPtr pDevModeOutput, HandleRef pDevModeInput, int fMode);

		// Token: 0x06000064 RID: 100
		[DllImport("winspool.drv", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DocumentProperties(HandleRef hwnd, HandleRef hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		// Token: 0x06000065 RID: 101
		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int EnumPrinters(int flags, string name, int level, IntPtr pPrinterEnum, int cbBuf, out int pcbNeeded, out int pcReturned);

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GlobalLock(HandleRef handle);

		// Token: 0x06000067 RID: 103
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr ResetDC(HandleRef hDC, HandleRef lpDevMode);

		// Token: 0x06000068 RID: 104
		[DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GlobalUnlock(HandleRef handle);

		// Token: 0x06000069 RID: 105
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x0600006A RID: 106 RVA: 0x00002A10 File Offset: 0x00000C10
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), SafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600006B RID: 107
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x0600006C RID: 108
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x0600006D RID: 109
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int AddFontResourceEx(string lpszFilename, int fl, IntPtr pdv);

		// Token: 0x0600006E RID: 110 RVA: 0x00002A25 File Offset: 0x00000C25
		public static int AddFontFile(string fileName)
		{
			return SafeNativeMethods.AddFontResourceEx(fileName, 16, IntPtr.Zero);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002A34 File Offset: 0x00000C34
		internal static IntPtr SaveClipRgn(IntPtr hDC)
		{
			IntPtr intPtr = SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				if (SafeNativeMethods.GetClipRgn(new HandleRef(null, hDC), new HandleRef(null, intPtr)) > 0)
				{
					intPtr2 = intPtr;
					intPtr = IntPtr.Zero;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}
			return intPtr2;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal static void RestoreClipRgn(IntPtr hDC, IntPtr hRgn)
		{
			try
			{
				SafeNativeMethods.SelectClipRgn(new HandleRef(null, hDC), new HandleRef(null, hRgn));
			}
			finally
			{
				if (hRgn != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, hRgn));
				}
			}
		}

		// Token: 0x06000071 RID: 113
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int ExtEscape(HandleRef hDC, int nEscape, int cbInput, ref int inData, int cbOutput, out int outData);

		// Token: 0x06000072 RID: 114
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int ExtEscape(HandleRef hDC, int nEscape, int cbInput, byte[] inData, int cbOutput, out int outData);

		// Token: 0x06000073 RID: 115
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);

		// Token: 0x06000074 RID: 116
		[DllImport("kernel32", CharSet = CharSet.Auto, EntryPoint = "GlobalAlloc", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntGlobalAlloc(int uFlags, UIntPtr dwBytes);

		// Token: 0x06000075 RID: 117 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static IntPtr GlobalAlloc(int uFlags, uint dwBytes)
		{
			return SafeNativeMethods.IntGlobalAlloc(uFlags, new UIntPtr(dwBytes));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002B00 File Offset: 0x00000D00
		internal unsafe static void ZeroMemory(byte* ptr, ulong length)
		{
			byte* ptr2 = ptr + length;
			while (ptr != ptr2)
			{
				*(ptr++) = 0;
			}
		}

		// Token: 0x06000077 RID: 119
		[DllImport("gdi32", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
		internal static extern int IntDeleteObject(HandleRef hObject);

		// Token: 0x06000078 RID: 120 RVA: 0x00002B20 File Offset: 0x00000D20
		public static int DeleteObject(HandleRef hObject)
		{
			global::System.Internal.HandleCollector.Remove((IntPtr)hObject, SafeNativeMethods.CommonHandles.GDI);
			return SafeNativeMethods.IntDeleteObject(hObject);
		}

		// Token: 0x06000079 RID: 121
		[DllImport("gdi32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);

		// Token: 0x0600007A RID: 122
		[DllImport("user32", EntryPoint = "CreateIconFromResourceEx", SetLastError = true)]
		private unsafe static extern IntPtr IntCreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags);

		// Token: 0x0600007B RID: 123 RVA: 0x00002B39 File Offset: 0x00000D39
		public unsafe static IntPtr CreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags)
		{
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateIconFromResourceEx(pbIconBits, cbIconBits, fIcon, dwVersion, csDesired, cyDesired, flags), SafeNativeMethods.CommonHandles.Icon);
		}

		// Token: 0x0600007C RID: 124
		[DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "ExtractAssociatedIcon")]
		public static extern IntPtr IntExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index);

		// Token: 0x0600007D RID: 125 RVA: 0x00002B54 File Offset: 0x00000D54
		public static IntPtr ExtractAssociatedIcon(HandleRef hInst, StringBuilder iconPath, ref int index)
		{
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntExtractAssociatedIcon(hInst, iconPath, ref index), SafeNativeMethods.CommonHandles.Icon);
		}

		// Token: 0x0600007E RID: 126
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "LoadIcon", SetLastError = true)]
		private static extern IntPtr IntLoadIcon(HandleRef hInst, IntPtr iconId);

		// Token: 0x0600007F RID: 127 RVA: 0x00002B68 File Offset: 0x00000D68
		public static IntPtr LoadIcon(HandleRef hInst, int iconId)
		{
			return SafeNativeMethods.IntLoadIcon(hInst, new IntPtr(iconId));
		}

		// Token: 0x06000080 RID: 128
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDestroyIcon(HandleRef hIcon);

		// Token: 0x06000081 RID: 129 RVA: 0x00002B76 File Offset: 0x00000D76
		public static bool DestroyIcon(HandleRef hIcon)
		{
			global::System.Internal.HandleCollector.Remove((IntPtr)hIcon, SafeNativeMethods.CommonHandles.Icon);
			return SafeNativeMethods.IntDestroyIcon(hIcon);
		}

		// Token: 0x06000082 RID: 130
		[DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "CopyImage", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags);

		// Token: 0x06000083 RID: 131 RVA: 0x00002B90 File Offset: 0x00000D90
		public static IntPtr CopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
		{
			int num;
			if (uType == 1)
			{
				num = SafeNativeMethods.CommonHandles.Icon;
			}
			else
			{
				num = SafeNativeMethods.CommonHandles.GDI;
			}
			return global::System.Internal.HandleCollector.Add(SafeNativeMethods.IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), num);
		}

		// Token: 0x06000084 RID: 132
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] SafeNativeMethods.BITMAP bm);

		// Token: 0x06000085 RID: 133
		[DllImport("gdi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] SafeNativeMethods.LOGFONT lf);

		// Token: 0x06000086 RID: 134 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static int GetObject(HandleRef hObject, SafeNativeMethods.LOGFONT lp)
		{
			return SafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof(SafeNativeMethods.LOGFONT)), lp);
		}

		// Token: 0x06000087 RID: 135
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GetIconInfo(HandleRef hIcon, [In] [Out] SafeNativeMethods.ICONINFO info);

		// Token: 0x06000088 RID: 136
		[DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);

		// Token: 0x06000089 RID: 137
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern SafeNativeMethods.IPicture OleCreatePictureIndirect(SafeNativeMethods.PICTDESC pictdesc, [In] ref Guid refiid, bool fOwn);

		// Token: 0x040000A0 RID: 160
		public const int ERROR_CANCELLED = 1223;

		// Token: 0x040000A1 RID: 161
		public const int E_UNEXPECTED = -2147418113;

		// Token: 0x040000A2 RID: 162
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x040000A3 RID: 163
		public const int E_ABORT = -2147467260;

		// Token: 0x040000A4 RID: 164
		public const int E_FAIL = -2147467259;

		// Token: 0x040000A5 RID: 165
		public const int E_ACCESSDENIED = -2147024891;

		// Token: 0x040000A6 RID: 166
		public const int GMEM_MOVEABLE = 2;

		// Token: 0x040000A7 RID: 167
		public const int GMEM_ZEROINIT = 64;

		// Token: 0x040000A8 RID: 168
		public const int DM_IN_BUFFER = 8;

		// Token: 0x040000A9 RID: 169
		public const int DM_OUT_BUFFER = 2;

		// Token: 0x040000AA RID: 170
		public const int DT_PLOTTER = 0;

		// Token: 0x040000AB RID: 171
		public const int DT_RASPRINTER = 2;

		// Token: 0x040000AC RID: 172
		public const int TECHNOLOGY = 2;

		// Token: 0x040000AD RID: 173
		public const int DC_PAPERS = 2;

		// Token: 0x040000AE RID: 174
		public const int DC_PAPERSIZE = 3;

		// Token: 0x040000AF RID: 175
		public const int DC_BINS = 6;

		// Token: 0x040000B0 RID: 176
		public const int DC_DUPLEX = 7;

		// Token: 0x040000B1 RID: 177
		public const int DC_BINNAMES = 12;

		// Token: 0x040000B2 RID: 178
		public const int DC_ENUMRESOLUTIONS = 13;

		// Token: 0x040000B3 RID: 179
		public const int DC_PAPERNAMES = 16;

		// Token: 0x040000B4 RID: 180
		public const int DC_ORIENTATION = 17;

		// Token: 0x040000B5 RID: 181
		public const int DC_COPIES = 18;

		// Token: 0x040000B6 RID: 182
		public const int PD_ALLPAGES = 0;

		// Token: 0x040000B7 RID: 183
		public const int PD_SELECTION = 1;

		// Token: 0x040000B8 RID: 184
		public const int PD_PAGENUMS = 2;

		// Token: 0x040000B9 RID: 185
		public const int PD_CURRENTPAGE = 4194304;

		// Token: 0x040000BA RID: 186
		public const int PD_RETURNDEFAULT = 1024;

		// Token: 0x040000BB RID: 187
		public const int DI_NORMAL = 3;

		// Token: 0x040000BC RID: 188
		public const int IMAGE_ICON = 1;

		// Token: 0x040000BD RID: 189
		public const int IDI_APPLICATION = 32512;

		// Token: 0x040000BE RID: 190
		public const int IDI_HAND = 32513;

		// Token: 0x040000BF RID: 191
		public const int IDI_QUESTION = 32514;

		// Token: 0x040000C0 RID: 192
		public const int IDI_EXCLAMATION = 32515;

		// Token: 0x040000C1 RID: 193
		public const int IDI_ASTERISK = 32516;

		// Token: 0x040000C2 RID: 194
		public const int IDI_WINLOGO = 32517;

		// Token: 0x040000C3 RID: 195
		public const int IDI_WARNING = 32515;

		// Token: 0x040000C4 RID: 196
		public const int IDI_ERROR = 32513;

		// Token: 0x040000C5 RID: 197
		public const int IDI_INFORMATION = 32516;

		// Token: 0x040000C6 RID: 198
		public const int SRCCOPY = 13369376;

		// Token: 0x040000C7 RID: 199
		public const int PLANES = 14;

		// Token: 0x040000C8 RID: 200
		public const int BITSPIXEL = 12;

		// Token: 0x040000C9 RID: 201
		public const int LOGPIXELSX = 88;

		// Token: 0x040000CA RID: 202
		public const int LOGPIXELSY = 90;

		// Token: 0x040000CB RID: 203
		public const int PHYSICALWIDTH = 110;

		// Token: 0x040000CC RID: 204
		public const int PHYSICALHEIGHT = 111;

		// Token: 0x040000CD RID: 205
		public const int PHYSICALOFFSETX = 112;

		// Token: 0x040000CE RID: 206
		public const int PHYSICALOFFSETY = 113;

		// Token: 0x040000CF RID: 207
		public const int VERTRES = 10;

		// Token: 0x040000D0 RID: 208
		public const int HORZRES = 8;

		// Token: 0x040000D1 RID: 209
		public const int DM_ORIENTATION = 1;

		// Token: 0x040000D2 RID: 210
		public const int DM_PAPERSIZE = 2;

		// Token: 0x040000D3 RID: 211
		public const int DM_PAPERLENGTH = 4;

		// Token: 0x040000D4 RID: 212
		public const int DM_PAPERWIDTH = 8;

		// Token: 0x040000D5 RID: 213
		public const int DM_COPIES = 256;

		// Token: 0x040000D6 RID: 214
		public const int DM_DEFAULTSOURCE = 512;

		// Token: 0x040000D7 RID: 215
		public const int DM_PRINTQUALITY = 1024;

		// Token: 0x040000D8 RID: 216
		public const int DM_COLOR = 2048;

		// Token: 0x040000D9 RID: 217
		public const int DM_DUPLEX = 4096;

		// Token: 0x040000DA RID: 218
		public const int DM_YRESOLUTION = 8192;

		// Token: 0x040000DB RID: 219
		public const int DM_COLLATE = 32768;

		// Token: 0x040000DC RID: 220
		public const int DMORIENT_PORTRAIT = 1;

		// Token: 0x040000DD RID: 221
		public const int DMORIENT_LANDSCAPE = 2;

		// Token: 0x040000DE RID: 222
		public const int DMPAPER_LETTER = 1;

		// Token: 0x040000DF RID: 223
		public const int DMPAPER_LETTERSMALL = 2;

		// Token: 0x040000E0 RID: 224
		public const int DMPAPER_TABLOID = 3;

		// Token: 0x040000E1 RID: 225
		public const int DMPAPER_LEDGER = 4;

		// Token: 0x040000E2 RID: 226
		public const int DMPAPER_LEGAL = 5;

		// Token: 0x040000E3 RID: 227
		public const int DMPAPER_STATEMENT = 6;

		// Token: 0x040000E4 RID: 228
		public const int DMPAPER_EXECUTIVE = 7;

		// Token: 0x040000E5 RID: 229
		public const int DMPAPER_A3 = 8;

		// Token: 0x040000E6 RID: 230
		public const int DMPAPER_A4 = 9;

		// Token: 0x040000E7 RID: 231
		public const int DMPAPER_A4SMALL = 10;

		// Token: 0x040000E8 RID: 232
		public const int DMPAPER_A5 = 11;

		// Token: 0x040000E9 RID: 233
		public const int DMPAPER_B4 = 12;

		// Token: 0x040000EA RID: 234
		public const int DMPAPER_B5 = 13;

		// Token: 0x040000EB RID: 235
		public const int DMPAPER_FOLIO = 14;

		// Token: 0x040000EC RID: 236
		public const int DMPAPER_QUARTO = 15;

		// Token: 0x040000ED RID: 237
		public const int DMPAPER_10X14 = 16;

		// Token: 0x040000EE RID: 238
		public const int DMPAPER_11X17 = 17;

		// Token: 0x040000EF RID: 239
		public const int DMPAPER_NOTE = 18;

		// Token: 0x040000F0 RID: 240
		public const int DMPAPER_ENV_9 = 19;

		// Token: 0x040000F1 RID: 241
		public const int DMPAPER_ENV_10 = 20;

		// Token: 0x040000F2 RID: 242
		public const int DMPAPER_ENV_11 = 21;

		// Token: 0x040000F3 RID: 243
		public const int DMPAPER_ENV_12 = 22;

		// Token: 0x040000F4 RID: 244
		public const int DMPAPER_ENV_14 = 23;

		// Token: 0x040000F5 RID: 245
		public const int DMPAPER_CSHEET = 24;

		// Token: 0x040000F6 RID: 246
		public const int DMPAPER_DSHEET = 25;

		// Token: 0x040000F7 RID: 247
		public const int DMPAPER_ESHEET = 26;

		// Token: 0x040000F8 RID: 248
		public const int DMPAPER_ENV_DL = 27;

		// Token: 0x040000F9 RID: 249
		public const int DMPAPER_ENV_C5 = 28;

		// Token: 0x040000FA RID: 250
		public const int DMPAPER_ENV_C3 = 29;

		// Token: 0x040000FB RID: 251
		public const int DMPAPER_ENV_C4 = 30;

		// Token: 0x040000FC RID: 252
		public const int DMPAPER_ENV_C6 = 31;

		// Token: 0x040000FD RID: 253
		public const int DMPAPER_ENV_C65 = 32;

		// Token: 0x040000FE RID: 254
		public const int DMPAPER_ENV_B4 = 33;

		// Token: 0x040000FF RID: 255
		public const int DMPAPER_ENV_B5 = 34;

		// Token: 0x04000100 RID: 256
		public const int DMPAPER_ENV_B6 = 35;

		// Token: 0x04000101 RID: 257
		public const int DMPAPER_ENV_ITALY = 36;

		// Token: 0x04000102 RID: 258
		public const int DMPAPER_ENV_MONARCH = 37;

		// Token: 0x04000103 RID: 259
		public const int DMPAPER_ENV_PERSONAL = 38;

		// Token: 0x04000104 RID: 260
		public const int DMPAPER_FANFOLD_US = 39;

		// Token: 0x04000105 RID: 261
		public const int DMPAPER_FANFOLD_STD_GERMAN = 40;

		// Token: 0x04000106 RID: 262
		public const int DMPAPER_FANFOLD_LGL_GERMAN = 41;

		// Token: 0x04000107 RID: 263
		public const int DMPAPER_ISO_B4 = 42;

		// Token: 0x04000108 RID: 264
		public const int DMPAPER_JAPANESE_POSTCARD = 43;

		// Token: 0x04000109 RID: 265
		public const int DMPAPER_9X11 = 44;

		// Token: 0x0400010A RID: 266
		public const int DMPAPER_10X11 = 45;

		// Token: 0x0400010B RID: 267
		public const int DMPAPER_15X11 = 46;

		// Token: 0x0400010C RID: 268
		public const int DMPAPER_ENV_INVITE = 47;

		// Token: 0x0400010D RID: 269
		public const int DMPAPER_RESERVED_48 = 48;

		// Token: 0x0400010E RID: 270
		public const int DMPAPER_RESERVED_49 = 49;

		// Token: 0x0400010F RID: 271
		public const int DMPAPER_LETTER_EXTRA = 50;

		// Token: 0x04000110 RID: 272
		public const int DMPAPER_LEGAL_EXTRA = 51;

		// Token: 0x04000111 RID: 273
		public const int DMPAPER_TABLOID_EXTRA = 52;

		// Token: 0x04000112 RID: 274
		public const int DMPAPER_A4_EXTRA = 53;

		// Token: 0x04000113 RID: 275
		public const int DMPAPER_LETTER_TRANSVERSE = 54;

		// Token: 0x04000114 RID: 276
		public const int DMPAPER_A4_TRANSVERSE = 55;

		// Token: 0x04000115 RID: 277
		public const int DMPAPER_LETTER_EXTRA_TRANSVERSE = 56;

		// Token: 0x04000116 RID: 278
		public const int DMPAPER_A_PLUS = 57;

		// Token: 0x04000117 RID: 279
		public const int DMPAPER_B_PLUS = 58;

		// Token: 0x04000118 RID: 280
		public const int DMPAPER_LETTER_PLUS = 59;

		// Token: 0x04000119 RID: 281
		public const int DMPAPER_A4_PLUS = 60;

		// Token: 0x0400011A RID: 282
		public const int DMPAPER_A5_TRANSVERSE = 61;

		// Token: 0x0400011B RID: 283
		public const int DMPAPER_B5_TRANSVERSE = 62;

		// Token: 0x0400011C RID: 284
		public const int DMPAPER_A3_EXTRA = 63;

		// Token: 0x0400011D RID: 285
		public const int DMPAPER_A5_EXTRA = 64;

		// Token: 0x0400011E RID: 286
		public const int DMPAPER_B5_EXTRA = 65;

		// Token: 0x0400011F RID: 287
		public const int DMPAPER_A2 = 66;

		// Token: 0x04000120 RID: 288
		public const int DMPAPER_A3_TRANSVERSE = 67;

		// Token: 0x04000121 RID: 289
		public const int DMPAPER_A3_EXTRA_TRANSVERSE = 68;

		// Token: 0x04000122 RID: 290
		public const int DMPAPER_DBL_JAPANESE_POSTCARD = 69;

		// Token: 0x04000123 RID: 291
		public const int DMPAPER_A6 = 70;

		// Token: 0x04000124 RID: 292
		public const int DMPAPER_JENV_KAKU2 = 71;

		// Token: 0x04000125 RID: 293
		public const int DMPAPER_JENV_KAKU3 = 72;

		// Token: 0x04000126 RID: 294
		public const int DMPAPER_JENV_CHOU3 = 73;

		// Token: 0x04000127 RID: 295
		public const int DMPAPER_JENV_CHOU4 = 74;

		// Token: 0x04000128 RID: 296
		public const int DMPAPER_LETTER_ROTATED = 75;

		// Token: 0x04000129 RID: 297
		public const int DMPAPER_A3_ROTATED = 76;

		// Token: 0x0400012A RID: 298
		public const int DMPAPER_A4_ROTATED = 77;

		// Token: 0x0400012B RID: 299
		public const int DMPAPER_A5_ROTATED = 78;

		// Token: 0x0400012C RID: 300
		public const int DMPAPER_B4_JIS_ROTATED = 79;

		// Token: 0x0400012D RID: 301
		public const int DMPAPER_B5_JIS_ROTATED = 80;

		// Token: 0x0400012E RID: 302
		public const int DMPAPER_JAPANESE_POSTCARD_ROTATED = 81;

		// Token: 0x0400012F RID: 303
		public const int DMPAPER_DBL_JAPANESE_POSTCARD_ROTATED = 82;

		// Token: 0x04000130 RID: 304
		public const int DMPAPER_A6_ROTATED = 83;

		// Token: 0x04000131 RID: 305
		public const int DMPAPER_JENV_KAKU2_ROTATED = 84;

		// Token: 0x04000132 RID: 306
		public const int DMPAPER_JENV_KAKU3_ROTATED = 85;

		// Token: 0x04000133 RID: 307
		public const int DMPAPER_JENV_CHOU3_ROTATED = 86;

		// Token: 0x04000134 RID: 308
		public const int DMPAPER_JENV_CHOU4_ROTATED = 87;

		// Token: 0x04000135 RID: 309
		public const int DMPAPER_B6_JIS = 88;

		// Token: 0x04000136 RID: 310
		public const int DMPAPER_B6_JIS_ROTATED = 89;

		// Token: 0x04000137 RID: 311
		public const int DMPAPER_12X11 = 90;

		// Token: 0x04000138 RID: 312
		public const int DMPAPER_JENV_YOU4 = 91;

		// Token: 0x04000139 RID: 313
		public const int DMPAPER_JENV_YOU4_ROTATED = 92;

		// Token: 0x0400013A RID: 314
		public const int DMPAPER_P16K = 93;

		// Token: 0x0400013B RID: 315
		public const int DMPAPER_P32K = 94;

		// Token: 0x0400013C RID: 316
		public const int DMPAPER_P32KBIG = 95;

		// Token: 0x0400013D RID: 317
		public const int DMPAPER_PENV_1 = 96;

		// Token: 0x0400013E RID: 318
		public const int DMPAPER_PENV_2 = 97;

		// Token: 0x0400013F RID: 319
		public const int DMPAPER_PENV_3 = 98;

		// Token: 0x04000140 RID: 320
		public const int DMPAPER_PENV_4 = 99;

		// Token: 0x04000141 RID: 321
		public const int DMPAPER_PENV_5 = 100;

		// Token: 0x04000142 RID: 322
		public const int DMPAPER_PENV_6 = 101;

		// Token: 0x04000143 RID: 323
		public const int DMPAPER_PENV_7 = 102;

		// Token: 0x04000144 RID: 324
		public const int DMPAPER_PENV_8 = 103;

		// Token: 0x04000145 RID: 325
		public const int DMPAPER_PENV_9 = 104;

		// Token: 0x04000146 RID: 326
		public const int DMPAPER_PENV_10 = 105;

		// Token: 0x04000147 RID: 327
		public const int DMPAPER_P16K_ROTATED = 106;

		// Token: 0x04000148 RID: 328
		public const int DMPAPER_P32K_ROTATED = 107;

		// Token: 0x04000149 RID: 329
		public const int DMPAPER_P32KBIG_ROTATED = 108;

		// Token: 0x0400014A RID: 330
		public const int DMPAPER_PENV_1_ROTATED = 109;

		// Token: 0x0400014B RID: 331
		public const int DMPAPER_PENV_2_ROTATED = 110;

		// Token: 0x0400014C RID: 332
		public const int DMPAPER_PENV_3_ROTATED = 111;

		// Token: 0x0400014D RID: 333
		public const int DMPAPER_PENV_4_ROTATED = 112;

		// Token: 0x0400014E RID: 334
		public const int DMPAPER_PENV_5_ROTATED = 113;

		// Token: 0x0400014F RID: 335
		public const int DMPAPER_PENV_6_ROTATED = 114;

		// Token: 0x04000150 RID: 336
		public const int DMPAPER_PENV_7_ROTATED = 115;

		// Token: 0x04000151 RID: 337
		public const int DMPAPER_PENV_8_ROTATED = 116;

		// Token: 0x04000152 RID: 338
		public const int DMPAPER_PENV_9_ROTATED = 117;

		// Token: 0x04000153 RID: 339
		public const int DMPAPER_PENV_10_ROTATED = 118;

		// Token: 0x04000154 RID: 340
		public const int DMPAPER_LAST = 118;

		// Token: 0x04000155 RID: 341
		public const int DMBIN_UPPER = 1;

		// Token: 0x04000156 RID: 342
		public const int DMBIN_LOWER = 2;

		// Token: 0x04000157 RID: 343
		public const int DMBIN_MIDDLE = 3;

		// Token: 0x04000158 RID: 344
		public const int DMBIN_MANUAL = 4;

		// Token: 0x04000159 RID: 345
		public const int DMBIN_ENVELOPE = 5;

		// Token: 0x0400015A RID: 346
		public const int DMBIN_ENVMANUAL = 6;

		// Token: 0x0400015B RID: 347
		public const int DMBIN_AUTO = 7;

		// Token: 0x0400015C RID: 348
		public const int DMBIN_TRACTOR = 8;

		// Token: 0x0400015D RID: 349
		public const int DMBIN_SMALLFMT = 9;

		// Token: 0x0400015E RID: 350
		public const int DMBIN_LARGEFMT = 10;

		// Token: 0x0400015F RID: 351
		public const int DMBIN_LARGECAPACITY = 11;

		// Token: 0x04000160 RID: 352
		public const int DMBIN_CASSETTE = 14;

		// Token: 0x04000161 RID: 353
		public const int DMBIN_FORMSOURCE = 15;

		// Token: 0x04000162 RID: 354
		public const int DMBIN_LAST = 15;

		// Token: 0x04000163 RID: 355
		public const int DMBIN_USER = 256;

		// Token: 0x04000164 RID: 356
		public const int DMRES_DRAFT = -1;

		// Token: 0x04000165 RID: 357
		public const int DMRES_LOW = -2;

		// Token: 0x04000166 RID: 358
		public const int DMRES_MEDIUM = -3;

		// Token: 0x04000167 RID: 359
		public const int DMRES_HIGH = -4;

		// Token: 0x04000168 RID: 360
		public const int DMCOLOR_MONOCHROME = 1;

		// Token: 0x04000169 RID: 361
		public const int DMCOLOR_COLOR = 2;

		// Token: 0x0400016A RID: 362
		public const int DMDUP_SIMPLEX = 1;

		// Token: 0x0400016B RID: 363
		public const int DMDUP_VERTICAL = 2;

		// Token: 0x0400016C RID: 364
		public const int DMDUP_HORIZONTAL = 3;

		// Token: 0x0400016D RID: 365
		public const int DMCOLLATE_FALSE = 0;

		// Token: 0x0400016E RID: 366
		public const int DMCOLLATE_TRUE = 1;

		// Token: 0x0400016F RID: 367
		public const int PRINTER_ENUM_LOCAL = 2;

		// Token: 0x04000170 RID: 368
		public const int PRINTER_ENUM_CONNECTIONS = 4;

		// Token: 0x04000171 RID: 369
		public const int SRCPAINT = 15597702;

		// Token: 0x04000172 RID: 370
		public const int SRCAND = 8913094;

		// Token: 0x04000173 RID: 371
		public const int SRCINVERT = 6684742;

		// Token: 0x04000174 RID: 372
		public const int SRCERASE = 4457256;

		// Token: 0x04000175 RID: 373
		public const int NOTSRCCOPY = 3342344;

		// Token: 0x04000176 RID: 374
		public const int NOTSRCERASE = 1114278;

		// Token: 0x04000177 RID: 375
		public const int MERGECOPY = 12583114;

		// Token: 0x04000178 RID: 376
		public const int MERGEPAINT = 12255782;

		// Token: 0x04000179 RID: 377
		public const int PATCOPY = 15728673;

		// Token: 0x0400017A RID: 378
		public const int PATPAINT = 16452105;

		// Token: 0x0400017B RID: 379
		public const int PATINVERT = 5898313;

		// Token: 0x0400017C RID: 380
		public const int DSTINVERT = 5570569;

		// Token: 0x0400017D RID: 381
		public const int BLACKNESS = 66;

		// Token: 0x0400017E RID: 382
		public const int WHITENESS = 16711778;

		// Token: 0x0400017F RID: 383
		public const int CAPTUREBLT = 1073741824;

		// Token: 0x04000180 RID: 384
		public const int SM_CXICON = 11;

		// Token: 0x04000181 RID: 385
		public const int SM_CYICON = 12;

		// Token: 0x04000182 RID: 386
		public const int DEFAULT_CHARSET = 1;

		// Token: 0x04000183 RID: 387
		public const int NOMIRRORBITMAP = -2147483648;

		// Token: 0x04000184 RID: 388
		public const int QUERYESCSUPPORT = 8;

		// Token: 0x04000185 RID: 389
		public const int CHECKJPEGFORMAT = 4119;

		// Token: 0x04000186 RID: 390
		public const int CHECKPNGFORMAT = 4120;

		// Token: 0x04000187 RID: 391
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000188 RID: 392
		public const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000189 RID: 393
		public const int ERROR_PROC_NOT_FOUND = 127;

		// Token: 0x02000019 RID: 25
		[SuppressUnmanagedCodeSecurity]
		internal class Gdip : GDIPlus
		{
			// Token: 0x0600008B RID: 139 RVA: 0x00002BD8 File Offset: 0x00000DD8
			static Gdip()
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				currentDomain.ProcessExit += SafeNativeMethods.Gdip.OnProcessExit;
				if (!currentDomain.IsDefaultAppDomain())
				{
					currentDomain.DomainUnload += SafeNativeMethods.Gdip.OnProcessExit;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600008C RID: 140 RVA: 0x00002C36 File Offset: 0x00000E36
			private static bool Initialized
			{
				get
				{
					return SafeNativeMethods.Gdip.s_initToken != IntPtr.Zero;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600008D RID: 141 RVA: 0x00002C48 File Offset: 0x00000E48
			internal static IDictionary ThreadData
			{
				get
				{
					LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("system.drawing.threaddata");
					IDictionary dictionary = (IDictionary)Thread.GetData(namedDataSlot);
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						Thread.SetData(namedDataSlot, dictionary);
					}
					return dictionary;
				}
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00002C7D File Offset: 0x00000E7D
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static void ClearThreadData()
			{
				Thread.SetData(Thread.GetNamedDataSlot("system.drawing.threaddata"), null);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00002C90 File Offset: 0x00000E90
			private static void Shutdown()
			{
				if (SafeNativeMethods.Gdip.Initialized)
				{
					SafeNativeMethods.Gdip.ClearThreadData();
					AppDomain currentDomain = AppDomain.CurrentDomain;
					currentDomain.ProcessExit -= SafeNativeMethods.Gdip.OnProcessExit;
					if (!currentDomain.IsDefaultAppDomain())
					{
						currentDomain.DomainUnload -= SafeNativeMethods.Gdip.OnProcessExit;
					}
				}
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00002CDB File Offset: 0x00000EDB
			[PrePrepareMethod]
			private static void OnProcessExit(object sender, EventArgs e)
			{
				SafeNativeMethods.Gdip.Shutdown();
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00002CE2 File Offset: 0x00000EE2
			internal static void DummyFunction()
			{
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00002CE4 File Offset: 0x00000EE4
			internal static void CheckStatus(int status)
			{
				if (status != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(status);
				}
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00002CF0 File Offset: 0x00000EF0
			internal static Exception StatusException(int status)
			{
				switch (status)
				{
				case 1:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 2:
					return new ArgumentException(SR.Format("Parameter is not valid.", Array.Empty<object>()));
				case 3:
					return new OutOfMemoryException(SR.Format("Out of memory.", Array.Empty<object>()));
				case 4:
					return new InvalidOperationException(SR.Format("Object is currently in use elsewhere.", Array.Empty<object>()));
				case 5:
					return new OutOfMemoryException(SR.Format("Buffer is too small (internal GDI+ error).", Array.Empty<object>()));
				case 6:
					return new NotImplementedException(SR.Format("Not implemented.", Array.Empty<object>()));
				case 7:
					return new ExternalException(SR.Format("A generic error occurred in GDI+.", Array.Empty<object>()), -2147467259);
				case 8:
					return new InvalidOperationException(SR.Format("Bitmap region is already locked.", Array.Empty<object>()));
				case 9:
					return new ExternalException(SR.Format("Function was ended.", Array.Empty<object>()), -2147467260);
				case 10:
					return new FileNotFoundException(SR.Format("File not found.", Array.Empty<object>()));
				case 11:
					return new OverflowException(SR.Format("Overflow error.", Array.Empty<object>()));
				case 12:
					return new ExternalException(SR.Format("File access is denied.", Array.Empty<object>()), -2147024891);
				case 13:
					return new ArgumentException(SR.Format("Image format is unknown.", Array.Empty<object>()));
				case 14:
					return new ArgumentException(SR.Format("Font '{0}' cannot be found.", new object[] { "?" }));
				case 15:
					return new ArgumentException(SR.Format("Font '{0}' does not support style '{1}'.", new object[] { "?", "?" }));
				case 16:
					return new ArgumentException(SR.Format("Only TrueType fonts are supported. This is not a TrueType font.", Array.Empty<object>()));
				case 17:
					return new ExternalException(SR.Format("Current version of GDI+ does not support this feature.", Array.Empty<object>()), -2147467259);
				case 18:
					return new ExternalException(SR.Format("GDI+ is not properly initialized (internal GDI+ error).", Array.Empty<object>()), -2147467259);
				case 19:
					return new ArgumentException(SR.Format("Property cannot be found.", Array.Empty<object>()));
				case 20:
					return new ArgumentException(SR.Format("Property is not supported.", Array.Empty<object>()));
				default:
					return new ExternalException(SR.Format("Unknown GDI+ error occurred.", Array.Empty<object>()), -2147418113);
				}
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00002F50 File Offset: 0x00001150
			internal static PointF[] ConvertGPPOINTFArrayF(IntPtr memory, int count)
			{
				if (memory == IntPtr.Zero)
				{
					throw new ArgumentNullException("memory");
				}
				PointF[] array = new PointF[count];
				Type typeFromHandle = typeof(GPPOINTF);
				int num = Marshal.SizeOf(typeFromHandle);
				for (int i = 0; i < count; i++)
				{
					GPPOINTF gppointf = (GPPOINTF)Marshal.PtrToStructure((IntPtr)((long)memory + (long)(i * num)), typeFromHandle);
					array[i] = new PointF(gppointf.X, gppointf.Y);
				}
				return array;
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00002FD4 File Offset: 0x000011D4
			internal static Point[] ConvertGPPOINTArray(IntPtr memory, int count)
			{
				if (memory == IntPtr.Zero)
				{
					throw new ArgumentNullException("memory");
				}
				Point[] array = new Point[count];
				Type typeFromHandle = typeof(GPPOINT);
				int num = Marshal.SizeOf(typeFromHandle);
				for (int i = 0; i < count; i++)
				{
					GPPOINT gppoint = (GPPOINT)Marshal.PtrToStructure((IntPtr)((long)memory + (long)(i * num)), typeFromHandle);
					array[i] = new Point(gppoint.X, gppoint.Y);
				}
				return array;
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00003058 File Offset: 0x00001258
			internal static IntPtr ConvertPointToMemory(PointF[] points)
			{
				if (points == null)
				{
					throw new ArgumentNullException("points");
				}
				int num = Marshal.SizeOf(typeof(GPPOINTF));
				int num2 = points.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					checked
					{
						Marshal.StructureToPtr<GPPOINTF>(new GPPOINTF(points[i]), (IntPtr)((long)intPtr + unchecked((long)(checked(i * num)))), false);
					}
				}
				return intPtr;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x000030C0 File Offset: 0x000012C0
			internal static IntPtr ConvertPointToMemory(Point[] points)
			{
				if (points == null)
				{
					throw new ArgumentNullException("points");
				}
				int num = Marshal.SizeOf(typeof(GPPOINT));
				int num2 = points.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					checked
					{
						Marshal.StructureToPtr<GPPOINT>(new GPPOINT(points[i]), (IntPtr)((long)intPtr + unchecked((long)(checked(i * num)))), false);
					}
				}
				return intPtr;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003128 File Offset: 0x00001328
			internal static IntPtr ConvertRectangleToMemory(RectangleF[] rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int num = Marshal.SizeOf(typeof(GPRECTF));
				int num2 = rect.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					checked
					{
						Marshal.StructureToPtr<GPRECTF>(new GPRECTF(rect[i]), (IntPtr)((long)intPtr + unchecked((long)(checked(i * num)))), false);
					}
				}
				return intPtr;
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00003190 File Offset: 0x00001390
			internal static IntPtr ConvertRectangleToMemory(Rectangle[] rect)
			{
				if (rect == null)
				{
					throw new ArgumentNullException("rect");
				}
				int num = Marshal.SizeOf(typeof(GPRECT));
				int num2 = rect.Length;
				IntPtr intPtr = Marshal.AllocHGlobal(checked(num2 * num));
				for (int i = 0; i < num2; i++)
				{
					checked
					{
						Marshal.StructureToPtr<GPRECT>(new GPRECT(rect[i]), (IntPtr)((long)intPtr + unchecked((long)(checked(i * num)))), false);
					}
				}
				return intPtr;
			}

			// Token: 0x0400018A RID: 394
			private static readonly TraceSwitch s_gdiPlusInitialization = new TraceSwitch("GdiPlusInitialization", "Tracks GDI+ initialization and teardown");

			// Token: 0x0400018B RID: 395
			private static IntPtr s_initToken = (IntPtr)1;

			// Token: 0x0400018C RID: 396
			private const string ThreadDataSlotName = "system.drawing.threaddata";

			// Token: 0x0400018D RID: 397
			internal const int Ok = 0;

			// Token: 0x0400018E RID: 398
			internal const int GenericError = 1;

			// Token: 0x0400018F RID: 399
			internal const int InvalidParameter = 2;

			// Token: 0x04000190 RID: 400
			internal const int OutOfMemory = 3;

			// Token: 0x04000191 RID: 401
			internal const int ObjectBusy = 4;

			// Token: 0x04000192 RID: 402
			internal const int InsufficientBuffer = 5;

			// Token: 0x04000193 RID: 403
			internal const int NotImplemented = 6;

			// Token: 0x04000194 RID: 404
			internal const int Win32Error = 7;

			// Token: 0x04000195 RID: 405
			internal const int WrongState = 8;

			// Token: 0x04000196 RID: 406
			internal const int Aborted = 9;

			// Token: 0x04000197 RID: 407
			internal const int FileNotFound = 10;

			// Token: 0x04000198 RID: 408
			internal const int ValueOverflow = 11;

			// Token: 0x04000199 RID: 409
			internal const int AccessDenied = 12;

			// Token: 0x0400019A RID: 410
			internal const int UnknownImageFormat = 13;

			// Token: 0x0400019B RID: 411
			internal const int FontFamilyNotFound = 14;

			// Token: 0x0400019C RID: 412
			internal const int FontStyleNotFound = 15;

			// Token: 0x0400019D RID: 413
			internal const int NotTrueTypeFont = 16;

			// Token: 0x0400019E RID: 414
			internal const int UnsupportedGdiplusVersion = 17;

			// Token: 0x0400019F RID: 415
			internal const int GdiplusNotInitialized = 18;

			// Token: 0x040001A0 RID: 416
			internal const int PropertyNotFound = 19;

			// Token: 0x040001A1 RID: 417
			internal const int PropertyNotSupported = 20;
		}

		// Token: 0x0200001A RID: 26
		[StructLayout(LayoutKind.Sequential)]
		public class ENHMETAHEADER
		{
			// Token: 0x040001A2 RID: 418
			public int iType;

			// Token: 0x040001A3 RID: 419
			public int nSize = 40;

			// Token: 0x040001A4 RID: 420
			public int rclBounds_left;

			// Token: 0x040001A5 RID: 421
			public int rclBounds_top;

			// Token: 0x040001A6 RID: 422
			public int rclBounds_right;

			// Token: 0x040001A7 RID: 423
			public int rclBounds_bottom;

			// Token: 0x040001A8 RID: 424
			public int rclFrame_left;

			// Token: 0x040001A9 RID: 425
			public int rclFrame_top;

			// Token: 0x040001AA RID: 426
			public int rclFrame_right;

			// Token: 0x040001AB RID: 427
			public int rclFrame_bottom;

			// Token: 0x040001AC RID: 428
			public int dSignature;

			// Token: 0x040001AD RID: 429
			public int nVersion;

			// Token: 0x040001AE RID: 430
			public int nBytes;

			// Token: 0x040001AF RID: 431
			public int nRecords;

			// Token: 0x040001B0 RID: 432
			public short nHandles;

			// Token: 0x040001B1 RID: 433
			public short sReserved;

			// Token: 0x040001B2 RID: 434
			public int nDescription;

			// Token: 0x040001B3 RID: 435
			public int offDescription;

			// Token: 0x040001B4 RID: 436
			public int nPalEntries;

			// Token: 0x040001B5 RID: 437
			public int szlDevice_cx;

			// Token: 0x040001B6 RID: 438
			public int szlDevice_cy;

			// Token: 0x040001B7 RID: 439
			public int szlMillimeters_cx;

			// Token: 0x040001B8 RID: 440
			public int szlMillimeters_cy;

			// Token: 0x040001B9 RID: 441
			public int cbPixelFormat;

			// Token: 0x040001BA RID: 442
			public int offPixelFormat;

			// Token: 0x040001BB RID: 443
			public int bOpenGL;
		}

		// Token: 0x0200001B RID: 27
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class DOCINFO
		{
			// Token: 0x040001BC RID: 444
			public int cbSize = 20;

			// Token: 0x040001BD RID: 445
			public string lpszDocName;

			// Token: 0x040001BE RID: 446
			public string lpszOutput;

			// Token: 0x040001BF RID: 447
			public string lpszDatatype;

			// Token: 0x040001C0 RID: 448
			public int fwType;
		}

		// Token: 0x0200001C RID: 28
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class PRINTDLG
		{
			// Token: 0x040001C1 RID: 449
			public int lStructSize;

			// Token: 0x040001C2 RID: 450
			public IntPtr hwndOwner;

			// Token: 0x040001C3 RID: 451
			public IntPtr hDevMode;

			// Token: 0x040001C4 RID: 452
			public IntPtr hDevNames;

			// Token: 0x040001C5 RID: 453
			public IntPtr hDC;

			// Token: 0x040001C6 RID: 454
			public int Flags;

			// Token: 0x040001C7 RID: 455
			public short nFromPage;

			// Token: 0x040001C8 RID: 456
			public short nToPage;

			// Token: 0x040001C9 RID: 457
			public short nMinPage;

			// Token: 0x040001CA RID: 458
			public short nMaxPage;

			// Token: 0x040001CB RID: 459
			public short nCopies;

			// Token: 0x040001CC RID: 460
			public IntPtr hInstance;

			// Token: 0x040001CD RID: 461
			public IntPtr lCustData;

			// Token: 0x040001CE RID: 462
			public IntPtr lpfnPrintHook;

			// Token: 0x040001CF RID: 463
			public IntPtr lpfnSetupHook;

			// Token: 0x040001D0 RID: 464
			public string lpPrintTemplateName;

			// Token: 0x040001D1 RID: 465
			public string lpSetupTemplateName;

			// Token: 0x040001D2 RID: 466
			public IntPtr hPrintTemplate;

			// Token: 0x040001D3 RID: 467
			public IntPtr hSetupTemplate;
		}

		// Token: 0x0200001D RID: 29
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class PRINTDLGX86
		{
			// Token: 0x040001D4 RID: 468
			public int lStructSize;

			// Token: 0x040001D5 RID: 469
			public IntPtr hwndOwner;

			// Token: 0x040001D6 RID: 470
			public IntPtr hDevMode;

			// Token: 0x040001D7 RID: 471
			public IntPtr hDevNames;

			// Token: 0x040001D8 RID: 472
			public IntPtr hDC;

			// Token: 0x040001D9 RID: 473
			public int Flags;

			// Token: 0x040001DA RID: 474
			public short nFromPage;

			// Token: 0x040001DB RID: 475
			public short nToPage;

			// Token: 0x040001DC RID: 476
			public short nMinPage;

			// Token: 0x040001DD RID: 477
			public short nMaxPage;

			// Token: 0x040001DE RID: 478
			public short nCopies;

			// Token: 0x040001DF RID: 479
			public IntPtr hInstance;

			// Token: 0x040001E0 RID: 480
			public IntPtr lCustData;

			// Token: 0x040001E1 RID: 481
			public IntPtr lpfnPrintHook;

			// Token: 0x040001E2 RID: 482
			public IntPtr lpfnSetupHook;

			// Token: 0x040001E3 RID: 483
			public string lpPrintTemplateName;

			// Token: 0x040001E4 RID: 484
			public string lpSetupTemplateName;

			// Token: 0x040001E5 RID: 485
			public IntPtr hPrintTemplate;

			// Token: 0x040001E6 RID: 486
			public IntPtr hSetupTemplate;
		}

		// Token: 0x0200001E RID: 30
		[StructLayout(LayoutKind.Sequential)]
		public class ICONINFO
		{
			// Token: 0x040001E7 RID: 487
			public int fIcon;

			// Token: 0x040001E8 RID: 488
			public int xHotspot;

			// Token: 0x040001E9 RID: 489
			public int yHotspot;

			// Token: 0x040001EA RID: 490
			public IntPtr hbmMask = IntPtr.Zero;

			// Token: 0x040001EB RID: 491
			public IntPtr hbmColor = IntPtr.Zero;
		}

		// Token: 0x0200001F RID: 31
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAP
		{
			// Token: 0x040001EC RID: 492
			public int bmType;

			// Token: 0x040001ED RID: 493
			public int bmWidth;

			// Token: 0x040001EE RID: 494
			public int bmHeight;

			// Token: 0x040001EF RID: 495
			public int bmWidthBytes;

			// Token: 0x040001F0 RID: 496
			public short bmPlanes;

			// Token: 0x040001F1 RID: 497
			public short bmBitsPixel;

			// Token: 0x040001F2 RID: 498
			public IntPtr bmBits = IntPtr.Zero;
		}

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAPINFOHEADER
		{
			// Token: 0x040001F3 RID: 499
			public int biSize = 40;

			// Token: 0x040001F4 RID: 500
			public int biWidth;

			// Token: 0x040001F5 RID: 501
			public int biHeight;

			// Token: 0x040001F6 RID: 502
			public short biPlanes;

			// Token: 0x040001F7 RID: 503
			public short biBitCount;

			// Token: 0x040001F8 RID: 504
			public int biCompression;

			// Token: 0x040001F9 RID: 505
			public int biSizeImage;

			// Token: 0x040001FA RID: 506
			public int biXPelsPerMeter;

			// Token: 0x040001FB RID: 507
			public int biYPelsPerMeter;

			// Token: 0x040001FC RID: 508
			public int biClrUsed;

			// Token: 0x040001FD RID: 509
			public int biClrImportant;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x00003260 File Offset: 0x00001460
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"lfHeight=", this.lfHeight, ", lfWidth=", this.lfWidth, ", lfEscapement=", this.lfEscapement, ", lfOrientation=", this.lfOrientation, ", lfWeight=", this.lfWeight,
					", lfItalic=", this.lfItalic, ", lfUnderline=", this.lfUnderline, ", lfStrikeOut=", this.lfStrikeOut, ", lfCharSet=", this.lfCharSet, ", lfOutPrecision=", this.lfOutPrecision,
					", lfClipPrecision=", this.lfClipPrecision, ", lfQuality=", this.lfQuality, ", lfPitchAndFamily=", this.lfPitchAndFamily, ", lfFaceName=", this.lfFaceName
				});
			}

			// Token: 0x040001FE RID: 510
			public int lfHeight;

			// Token: 0x040001FF RID: 511
			public int lfWidth;

			// Token: 0x04000200 RID: 512
			public int lfEscapement;

			// Token: 0x04000201 RID: 513
			public int lfOrientation;

			// Token: 0x04000202 RID: 514
			public int lfWeight;

			// Token: 0x04000203 RID: 515
			public byte lfItalic;

			// Token: 0x04000204 RID: 516
			public byte lfUnderline;

			// Token: 0x04000205 RID: 517
			public byte lfStrikeOut;

			// Token: 0x04000206 RID: 518
			public byte lfCharSet;

			// Token: 0x04000207 RID: 519
			public byte lfOutPrecision;

			// Token: 0x04000208 RID: 520
			public byte lfClipPrecision;

			// Token: 0x04000209 RID: 521
			public byte lfQuality;

			// Token: 0x0400020A RID: 522
			public byte lfPitchAndFamily;

			// Token: 0x0400020B RID: 523
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x02000022 RID: 34
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct ICONDIR
		{
			// Token: 0x0400020C RID: 524
			public short idReserved;

			// Token: 0x0400020D RID: 525
			public short idType;

			// Token: 0x0400020E RID: 526
			public short idCount;

			// Token: 0x0400020F RID: 527
			public SafeNativeMethods.ICONDIRENTRY idEntries;
		}

		// Token: 0x02000023 RID: 35
		public struct ICONDIRENTRY
		{
			// Token: 0x04000210 RID: 528
			public byte bWidth;

			// Token: 0x04000211 RID: 529
			public byte bHeight;

			// Token: 0x04000212 RID: 530
			public byte bColorCount;

			// Token: 0x04000213 RID: 531
			public byte bReserved;

			// Token: 0x04000214 RID: 532
			public short wPlanes;

			// Token: 0x04000215 RID: 533
			public short wBitCount;

			// Token: 0x04000216 RID: 534
			public int dwBytesInRes;

			// Token: 0x04000217 RID: 535
			public int dwImageOffset;
		}

		// Token: 0x02000024 RID: 36
		public class Ole
		{
			// Token: 0x04000218 RID: 536
			public const int PICTYPE_ICON = 3;
		}

		// Token: 0x02000025 RID: 37
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESC
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x000033BB File Offset: 0x000015BB
			public static SafeNativeMethods.PICTDESC CreateIconPICTDESC(IntPtr hicon)
			{
				return new SafeNativeMethods.PICTDESC
				{
					cbSizeOfStruct = 12,
					picType = 3,
					union1 = hicon
				};
			}

			// Token: 0x04000219 RID: 537
			internal int cbSizeOfStruct;

			// Token: 0x0400021A RID: 538
			public int picType;

			// Token: 0x0400021B RID: 539
			internal IntPtr union1;

			// Token: 0x0400021C RID: 540
			internal int union2;

			// Token: 0x0400021D RID: 541
			internal int union3;
		}

		// Token: 0x02000026 RID: 38
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class DEVMODE
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x000033D8 File Offset: 0x000015D8
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"[DEVMODE: dmDeviceName=", this.dmDeviceName, ", dmSpecVersion=", this.dmSpecVersion, ", dmDriverVersion=", this.dmDriverVersion, ", dmSize=", this.dmSize, ", dmDriverExtra=", this.dmDriverExtra,
					", dmFields=", this.dmFields, ", dmOrientation=", this.dmOrientation, ", dmPaperSize=", this.dmPaperSize, ", dmPaperLength=", this.dmPaperLength, ", dmPaperWidth=", this.dmPaperWidth,
					", dmScale=", this.dmScale, ", dmCopies=", this.dmCopies, ", dmDefaultSource=", this.dmDefaultSource, ", dmPrintQuality=", this.dmPrintQuality, ", dmColor=", this.dmColor,
					", dmDuplex=", this.dmDuplex, ", dmYResolution=", this.dmYResolution, ", dmTTOption=", this.dmTTOption, ", dmCollate=", this.dmCollate, ", dmFormName=", this.dmFormName,
					", dmLogPixels=", this.dmLogPixels, ", dmBitsPerPel=", this.dmBitsPerPel, ", dmPelsWidth=", this.dmPelsWidth, ", dmPelsHeight=", this.dmPelsHeight, ", dmDisplayFlags=", this.dmDisplayFlags,
					", dmDisplayFrequency=", this.dmDisplayFrequency, ", dmICMMethod=", this.dmICMMethod, ", dmICMIntent=", this.dmICMIntent, ", dmMediaType=", this.dmMediaType, ", dmDitherType=", this.dmDitherType,
					", dmICCManufacturer=", this.dmICCManufacturer, ", dmICCModel=", this.dmICCModel, ", dmPanningWidth=", this.dmPanningWidth, ", dmPanningHeight=", this.dmPanningHeight, "]"
				});
			}

			// Token: 0x0400021E RID: 542
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x0400021F RID: 543
			public short dmSpecVersion;

			// Token: 0x04000220 RID: 544
			public short dmDriverVersion;

			// Token: 0x04000221 RID: 545
			public short dmSize;

			// Token: 0x04000222 RID: 546
			public short dmDriverExtra;

			// Token: 0x04000223 RID: 547
			public int dmFields;

			// Token: 0x04000224 RID: 548
			public short dmOrientation;

			// Token: 0x04000225 RID: 549
			public short dmPaperSize;

			// Token: 0x04000226 RID: 550
			public short dmPaperLength;

			// Token: 0x04000227 RID: 551
			public short dmPaperWidth;

			// Token: 0x04000228 RID: 552
			public short dmScale;

			// Token: 0x04000229 RID: 553
			public short dmCopies;

			// Token: 0x0400022A RID: 554
			public short dmDefaultSource;

			// Token: 0x0400022B RID: 555
			public short dmPrintQuality;

			// Token: 0x0400022C RID: 556
			public short dmColor;

			// Token: 0x0400022D RID: 557
			public short dmDuplex;

			// Token: 0x0400022E RID: 558
			public short dmYResolution;

			// Token: 0x0400022F RID: 559
			public short dmTTOption;

			// Token: 0x04000230 RID: 560
			public short dmCollate;

			// Token: 0x04000231 RID: 561
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x04000232 RID: 562
			public short dmLogPixels;

			// Token: 0x04000233 RID: 563
			public int dmBitsPerPel;

			// Token: 0x04000234 RID: 564
			public int dmPelsWidth;

			// Token: 0x04000235 RID: 565
			public int dmPelsHeight;

			// Token: 0x04000236 RID: 566
			public int dmDisplayFlags;

			// Token: 0x04000237 RID: 567
			public int dmDisplayFrequency;

			// Token: 0x04000238 RID: 568
			public int dmICMMethod;

			// Token: 0x04000239 RID: 569
			public int dmICMIntent;

			// Token: 0x0400023A RID: 570
			public int dmMediaType;

			// Token: 0x0400023B RID: 571
			public int dmDitherType;

			// Token: 0x0400023C RID: 572
			public int dmICCManufacturer;

			// Token: 0x0400023D RID: 573
			public int dmICCModel;

			// Token: 0x0400023E RID: 574
			public int dmPanningWidth;

			// Token: 0x0400023F RID: 575
			public int dmPanningHeight;
		}

		// Token: 0x02000027 RID: 39
		public sealed class CommonHandles
		{
			// Token: 0x04000240 RID: 576
			public static readonly int GDI = global::System.Internal.HandleCollector.RegisterType("GDI", 50, 500);

			// Token: 0x04000241 RID: 577
			public static readonly int HDC = global::System.Internal.HandleCollector.RegisterType("HDC", 100, 2);

			// Token: 0x04000242 RID: 578
			public static readonly int Icon = global::System.Internal.HandleCollector.RegisterType("Icon", 20, 500);

			// Token: 0x04000243 RID: 579
			public static readonly int Kernel = global::System.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);
		}

		// Token: 0x02000028 RID: 40
		public class StreamConsts
		{
			// Token: 0x04000244 RID: 580
			public const int STREAM_SEEK_SET = 0;

			// Token: 0x04000245 RID: 581
			public const int STREAM_SEEK_CUR = 1;

			// Token: 0x04000246 RID: 582
			public const int STREAM_SEEK_END = 2;
		}

		// Token: 0x02000029 RID: 41
		[Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPicture
		{
			// Token: 0x060000AC RID: 172
			[SuppressUnmanagedCodeSecurity]
			IntPtr GetHandle();

			// Token: 0x060000AD RID: 173
			[SuppressUnmanagedCodeSecurity]
			IntPtr GetHPal();

			// Token: 0x060000AE RID: 174
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.I2)]
			short GetPictureType();

			// Token: 0x060000AF RID: 175
			[SuppressUnmanagedCodeSecurity]
			int GetWidth();

			// Token: 0x060000B0 RID: 176
			[SuppressUnmanagedCodeSecurity]
			int GetHeight();

			// Token: 0x060000B1 RID: 177
			[SuppressUnmanagedCodeSecurity]
			void Render();

			// Token: 0x060000B2 RID: 178
			[SuppressUnmanagedCodeSecurity]
			void SetHPal([In] IntPtr phpal);

			// Token: 0x060000B3 RID: 179
			[SuppressUnmanagedCodeSecurity]
			IntPtr GetCurDC();

			// Token: 0x060000B4 RID: 180
			[SuppressUnmanagedCodeSecurity]
			void SelectPicture([In] IntPtr hdcIn, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] phdcOut, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] phbmpOut);

			// Token: 0x060000B5 RID: 181
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetKeepOriginalFormat();

			// Token: 0x060000B6 RID: 182
			[SuppressUnmanagedCodeSecurity]
			void SetKeepOriginalFormat([MarshalAs(UnmanagedType.Bool)] [In] bool pfkeep);

			// Token: 0x060000B7 RID: 183
			[SuppressUnmanagedCodeSecurity]
			void PictureChanged();

			// Token: 0x060000B8 RID: 184
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int SaveAsFile([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [In] int fSaveMemCopy, out int pcbSize);

			// Token: 0x060000B9 RID: 185
			[SuppressUnmanagedCodeSecurity]
			int GetAttributes();

			// Token: 0x060000BA RID: 186
			[SuppressUnmanagedCodeSecurity]
			void SetHdc([In] IntPtr hdc);
		}
	}
}
