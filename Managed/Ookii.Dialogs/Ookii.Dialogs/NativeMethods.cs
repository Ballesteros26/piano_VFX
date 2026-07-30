using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000010 RID: 16
	internal static class NativeMethods
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004B30 File Offset: 0x00002D30
		public static bool IsWindowsVistaOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version >= new Version(6, 0, 6000);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004B70 File Offset: 0x00002D70
		public static bool IsWindowsXPOrLater
		{
			get
			{
				return Environment.OSVersion.Platform == 2 && Environment.OSVersion.Version >= new Version(5, 1, 2600);
			}
		}

		// Token: 0x0600009E RID: 158
		[DllImport("kernel32", CharSet = 3, SetLastError = true)]
		public static extern SafeModuleHandle LoadLibraryEx(string lpFileName, IntPtr hFile, NativeMethods.LoadLibraryExFlags dwFlags);

		// Token: 0x0600009F RID: 159
		[ReliabilityContract(3, 2)]
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060000A0 RID: 160
		[DllImport("user32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		// Token: 0x060000A1 RID: 161
		[DllImport("user32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		// Token: 0x060000A2 RID: 162
		[DllImport("kernel32.dll", CharSet = 4, ExactSpelling = true)]
		public static extern int GetCurrentThreadId();

		// Token: 0x060000A3 RID: 163
		[DllImport("comctl32.dll", PreserveSig = false)]
		public static extern void TaskDialogIndirect([In] ref NativeMethods.TASKDIALOGCONFIG pTaskConfig, out int pnButton, out int pnRadioButton, [MarshalAs(2)] out bool pfVerificationFlagChecked);

		// Token: 0x060000A4 RID: 164
		[DllImport("user32.dll", CharSet = 4)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060000A5 RID: 165
		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern ActivationContextSafeHandle CreateActCtx(ref NativeMethods.ACTCTX actctx);

		// Token: 0x060000A6 RID: 166
		[ReliabilityContract(3, 1)]
		[DllImport("kernel32.dll")]
		public static extern void ReleaseActCtx(IntPtr hActCtx);

		// Token: 0x060000A7 RID: 167
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool ActivateActCtx(ActivationContextSafeHandle hActCtx, out IntPtr lpCookie);

		// Token: 0x060000A8 RID: 168
		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);

		// Token: 0x060000A9 RID: 169
		[DllImport("shell32.dll", CharSet = 3)]
		public static extern int SHCreateItemFromParsingName([MarshalAs(21)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(28)] out object ppv);

		// Token: 0x060000AA RID: 170 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public static IShellItem CreateItemFromParsingName(string path)
		{
			Guid guid;
			guid..ctor("43826d1e-e718-42ee-bc55-a1e261c37bfe");
			object obj;
			int num = NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out obj);
			bool flag = num != 0;
			if (flag)
			{
				throw new Win32Exception(num);
			}
			return (IShellItem)obj;
		}

		// Token: 0x060000AB RID: 171
		[DllImport("user32.dll", BestFitMapping = false, CharSet = 4, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern int LoadString(SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

		// Token: 0x060000AC RID: 172
		[DllImport("Kernel32.dll", CharSet = 4, SetLastError = true)]
		public static extern uint FormatMessage([MarshalAs(8)] NativeMethods.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, string[] Arguments);

		// Token: 0x060000AD RID: 173
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, [In] ref NativeMethods.MARGINS pMarInset);

		// Token: 0x060000AE RID: 174
		[DllImport("dwmapi.dll", PreserveSig = false)]
		[return: MarshalAs(2)]
		public static extern bool DwmIsCompositionEnabled();

		// Token: 0x060000AF RID: 175
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern SafeDeviceHandle CreateCompatibleDC(IntPtr hDC);

		// Token: 0x060000B0 RID: 176
		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(SafeDeviceHandle hDC, SafeGDIHandle hObject);

		// Token: 0x060000B1 RID: 177
		[ReliabilityContract(3, 2)]
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x060000B2 RID: 178
		[ReliabilityContract(3, 2)]
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x060000B3 RID: 179
		[DllImport("gdi32.dll")]
		[return: MarshalAs(2)]
		public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, SafeDeviceHandle hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		// Token: 0x060000B4 RID: 180
		[DllImport("UxTheme.dll", CharSet = 3, PreserveSig = false)]
		public static extern void DrawThemeTextEx(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref NativeMethods.RECT pRect, ref NativeMethods.DTTOPTS pOptions);

		// Token: 0x060000B5 RID: 181
		[DllImport("gdi32.dll")]
		public static extern SafeGDIHandle CreateDIBSection(IntPtr hdc, NativeMethods.BITMAPINFO pbmi, uint iUsage, IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x060000B6 RID: 182
		[DllImport("UxTheme.dll", CharSet = 3, PreserveSig = false)]
		public static extern void GetThemeTextExtent(IntPtr hTheme, SafeDeviceHandle hdc, int iPartId, int iStateId, string text, int iCharCount, int dwTextFlags, [In] ref NativeMethods.RECT bounds, out NativeMethods.RECT rect);

		// Token: 0x060000B7 RID: 183 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public static SafeGDIHandle CreateDib(Rectangle bounds, IntPtr primaryHdc, SafeDeviceHandle memoryHdc)
		{
			NativeMethods.BITMAPINFO bitmapinfo = new NativeMethods.BITMAPINFO();
			bitmapinfo.biSize = Marshal.SizeOf(bitmapinfo);
			bitmapinfo.biWidth = bounds.Width;
			bitmapinfo.biHeight = -bounds.Height;
			bitmapinfo.biPlanes = 1;
			bitmapinfo.biBitCount = 32;
			bitmapinfo.biCompression = 0;
			SafeGDIHandle safeGDIHandle = NativeMethods.CreateDIBSection(primaryHdc, bitmapinfo, 0U, IntPtr.Zero, IntPtr.Zero, 0U);
			NativeMethods.SelectObject(memoryHdc, safeGDIHandle);
			return safeGDIHandle;
		}

		// Token: 0x060000B8 RID: 184
		[DllImport("credui.dll", CharSet = 3)]
		internal static extern NativeMethods.CredUIReturnCodes CredUIPromptForCredentials(ref NativeMethods.CREDUI_INFO pUiInfo, string targetName, IntPtr Reserved, int dwAuthError, StringBuilder pszUserName, uint ulUserNameMaxChars, StringBuilder pszPassword, uint ulPaswordMaxChars, [MarshalAs(2)] [In] [Out] ref bool pfSave, NativeMethods.CREDUI_FLAGS dwFlags);

		// Token: 0x060000B9 RID: 185
		[DllImport("credui.dll", CharSet = 3)]
		public static extern NativeMethods.CredUIReturnCodes CredUIPromptForWindowsCredentials(ref NativeMethods.CREDUI_INFO pUiInfo, uint dwAuthError, ref uint pulAuthPackage, IntPtr pvInAuthBuffer, uint ulInAuthBufferSize, out IntPtr ppvOutAuthBuffer, out uint pulOutAuthBufferSize, [MarshalAs(2)] ref bool pfSave, NativeMethods.CredUIWinFlags dwFlags);

		// Token: 0x060000BA RID: 186
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredReadW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredRead(string TargetName, NativeMethods.CredTypes Type, int Flags, out IntPtr Credential);

		// Token: 0x060000BB RID: 187
		[ReliabilityContract(3, 2)]
		[DllImport("advapi32.dll")]
		internal static extern void CredFree(IntPtr Buffer);

		// Token: 0x060000BC RID: 188
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredDeleteW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredDelete(string TargetName, NativeMethods.CredTypes Type, int Flags);

		// Token: 0x060000BD RID: 189
		[DllImport("advapi32.dll", CharSet = 3, EntryPoint = "CredWriteW", SetLastError = true)]
		[return: MarshalAs(2)]
		internal static extern bool CredWrite(ref NativeMethods.CREDENTIAL Credential, int Flags);

		// Token: 0x060000BE RID: 190
		[DllImport("credui.dll", CharSet = 3, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool CredPackAuthenticationBuffer(uint dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, ref uint pcbPackedCredentials);

		// Token: 0x060000BF RID: 191
		[DllImport("credui.dll", CharSet = 3, SetLastError = true)]
		[return: MarshalAs(2)]
		public static extern bool CredUnPackAuthenticationBuffer(uint dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref uint pcchMaxUserName, StringBuilder pszDomainName, ref uint pcchMaxDomainName, StringBuilder pszPassword, ref uint pcchMaxPassword);

		// Token: 0x0400003F RID: 63
		public const int ErrorFileNotFound = 2;

		// Token: 0x04000040 RID: 64
		public const int WM_USER = 1024;

		// Token: 0x04000041 RID: 65
		public const int WM_GETICON = 127;

		// Token: 0x04000042 RID: 66
		public const int WM_SETICON = 128;

		// Token: 0x04000043 RID: 67
		public const int ICON_SMALL = 0;

		// Token: 0x04000044 RID: 68
		public const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4;

		// Token: 0x04000045 RID: 69
		public const int WM_NCHITTEST = 132;

		// Token: 0x04000046 RID: 70
		public const int WM_DWMCOMPOSITIONCHANGED = 798;

		// Token: 0x04000047 RID: 71
		internal const int CREDUI_MAX_USERNAME_LENGTH = 513;

		// Token: 0x04000048 RID: 72
		internal const int CREDUI_MAX_PASSWORD_LENGTH = 256;

		// Token: 0x0200004A RID: 74
		[Flags]
		public enum LoadLibraryExFlags : uint
		{
			// Token: 0x040000F4 RID: 244
			DontResolveDllReferences = 1U,
			// Token: 0x040000F5 RID: 245
			LoadLibraryAsDatafile = 2U,
			// Token: 0x040000F6 RID: 246
			LoadWithAlteredSearchPath = 8U,
			// Token: 0x040000F7 RID: 247
			LoadIgnoreCodeAuthzLevel = 16U
		}

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060002F3 RID: 755
		public delegate uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData);

		// Token: 0x0200004C RID: 76
		public enum TaskDialogNotifications
		{
			// Token: 0x040000F9 RID: 249
			Created,
			// Token: 0x040000FA RID: 250
			Navigated,
			// Token: 0x040000FB RID: 251
			ButtonClicked,
			// Token: 0x040000FC RID: 252
			HyperlinkClicked,
			// Token: 0x040000FD RID: 253
			Timer,
			// Token: 0x040000FE RID: 254
			Destroyed,
			// Token: 0x040000FF RID: 255
			RadioButtonClicked,
			// Token: 0x04000100 RID: 256
			DialogConstructed,
			// Token: 0x04000101 RID: 257
			VerificationClicked,
			// Token: 0x04000102 RID: 258
			Help,
			// Token: 0x04000103 RID: 259
			ExpandoButtonClicked
		}

		// Token: 0x0200004D RID: 77
		[Flags]
		public enum TaskDialogCommonButtonFlags
		{
			// Token: 0x04000105 RID: 261
			OkButton = 1,
			// Token: 0x04000106 RID: 262
			YesButton = 2,
			// Token: 0x04000107 RID: 263
			NoButton = 4,
			// Token: 0x04000108 RID: 264
			CancelButton = 8,
			// Token: 0x04000109 RID: 265
			RetryButton = 16,
			// Token: 0x0400010A RID: 266
			CloseButton = 32
		}

		// Token: 0x0200004E RID: 78
		[Flags]
		public enum TaskDialogFlags
		{
			// Token: 0x0400010C RID: 268
			EnableHyperLinks = 1,
			// Token: 0x0400010D RID: 269
			UseHIconMain = 2,
			// Token: 0x0400010E RID: 270
			UseHIconFooter = 4,
			// Token: 0x0400010F RID: 271
			AllowDialogCancellation = 8,
			// Token: 0x04000110 RID: 272
			UseCommandLinks = 16,
			// Token: 0x04000111 RID: 273
			UseCommandLinksNoIcon = 32,
			// Token: 0x04000112 RID: 274
			ExpandFooterArea = 64,
			// Token: 0x04000113 RID: 275
			ExpandedByDefault = 128,
			// Token: 0x04000114 RID: 276
			VerificationFlagChecked = 256,
			// Token: 0x04000115 RID: 277
			ShowProgressBar = 512,
			// Token: 0x04000116 RID: 278
			ShowMarqueeProgressBar = 1024,
			// Token: 0x04000117 RID: 279
			CallbackTimer = 2048,
			// Token: 0x04000118 RID: 280
			PositionRelativeToWindow = 4096,
			// Token: 0x04000119 RID: 281
			RtlLayout = 8192,
			// Token: 0x0400011A RID: 282
			NoDefaultRadioButton = 16384,
			// Token: 0x0400011B RID: 283
			CanBeMinimized = 32768
		}

		// Token: 0x0200004F RID: 79
		public enum TaskDialogMessages
		{
			// Token: 0x0400011D RID: 285
			NavigatePage = 1125,
			// Token: 0x0400011E RID: 286
			ClickButton,
			// Token: 0x0400011F RID: 287
			SetMarqueeProgressBar,
			// Token: 0x04000120 RID: 288
			SetProgressBarState,
			// Token: 0x04000121 RID: 289
			SetProgressBarRange,
			// Token: 0x04000122 RID: 290
			SetProgressBarPos,
			// Token: 0x04000123 RID: 291
			SetProgressBarMarquee,
			// Token: 0x04000124 RID: 292
			SetElementText,
			// Token: 0x04000125 RID: 293
			ClickRadioButton = 1134,
			// Token: 0x04000126 RID: 294
			EnableButton,
			// Token: 0x04000127 RID: 295
			EnableRadioButton,
			// Token: 0x04000128 RID: 296
			ClickVerification,
			// Token: 0x04000129 RID: 297
			UpdateElementText,
			// Token: 0x0400012A RID: 298
			SetButtonElevationRequiredState,
			// Token: 0x0400012B RID: 299
			UpdateIcon
		}

		// Token: 0x02000050 RID: 80
		public enum TaskDialogElements
		{
			// Token: 0x0400012D RID: 301
			Content,
			// Token: 0x0400012E RID: 302
			ExpandedInformation,
			// Token: 0x0400012F RID: 303
			Footer,
			// Token: 0x04000130 RID: 304
			MainInstruction
		}

		// Token: 0x02000051 RID: 81
		[StructLayout(0, Pack = 4)]
		public struct TASKDIALOG_BUTTON
		{
			// Token: 0x04000131 RID: 305
			public int nButtonID;

			// Token: 0x04000132 RID: 306
			[MarshalAs(21)]
			public string pszButtonText;
		}

		// Token: 0x02000052 RID: 82
		[StructLayout(0, Pack = 4)]
		public struct TASKDIALOGCONFIG
		{
			// Token: 0x04000133 RID: 307
			public uint cbSize;

			// Token: 0x04000134 RID: 308
			public IntPtr hwndParent;

			// Token: 0x04000135 RID: 309
			public IntPtr hInstance;

			// Token: 0x04000136 RID: 310
			public NativeMethods.TaskDialogFlags dwFlags;

			// Token: 0x04000137 RID: 311
			public NativeMethods.TaskDialogCommonButtonFlags dwCommonButtons;

			// Token: 0x04000138 RID: 312
			[MarshalAs(21)]
			public string pszWindowTitle;

			// Token: 0x04000139 RID: 313
			public IntPtr hMainIcon;

			// Token: 0x0400013A RID: 314
			[MarshalAs(21)]
			public string pszMainInstruction;

			// Token: 0x0400013B RID: 315
			[MarshalAs(21)]
			public string pszContent;

			// Token: 0x0400013C RID: 316
			public uint cButtons;

			// Token: 0x0400013D RID: 317
			public IntPtr pButtons;

			// Token: 0x0400013E RID: 318
			public int nDefaultButton;

			// Token: 0x0400013F RID: 319
			public uint cRadioButtons;

			// Token: 0x04000140 RID: 320
			public IntPtr pRadioButtons;

			// Token: 0x04000141 RID: 321
			public int nDefaultRadioButton;

			// Token: 0x04000142 RID: 322
			[MarshalAs(21)]
			public string pszVerificationText;

			// Token: 0x04000143 RID: 323
			[MarshalAs(21)]
			public string pszExpandedInformation;

			// Token: 0x04000144 RID: 324
			[MarshalAs(21)]
			public string pszExpandedControlText;

			// Token: 0x04000145 RID: 325
			[MarshalAs(21)]
			public string pszCollapsedControlText;

			// Token: 0x04000146 RID: 326
			public IntPtr hFooterIcon;

			// Token: 0x04000147 RID: 327
			[MarshalAs(21)]
			public string pszFooterText;

			// Token: 0x04000148 RID: 328
			[MarshalAs(38)]
			public NativeMethods.TaskDialogCallback pfCallback;

			// Token: 0x04000149 RID: 329
			public IntPtr lpCallbackData;

			// Token: 0x0400014A RID: 330
			public uint cxWidth;
		}

		// Token: 0x02000053 RID: 83
		public struct ACTCTX
		{
			// Token: 0x0400014B RID: 331
			public int cbSize;

			// Token: 0x0400014C RID: 332
			public uint dwFlags;

			// Token: 0x0400014D RID: 333
			public string lpSource;

			// Token: 0x0400014E RID: 334
			public ushort wProcessorArchitecture;

			// Token: 0x0400014F RID: 335
			public ushort wLangId;

			// Token: 0x04000150 RID: 336
			public string lpAssemblyDirectory;

			// Token: 0x04000151 RID: 337
			public string lpResourceName;

			// Token: 0x04000152 RID: 338
			public string lpApplicationName;
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(0, CharSet = 4, Pack = 4)]
		internal struct COMDLG_FILTERSPEC
		{
			// Token: 0x04000153 RID: 339
			[MarshalAs(21)]
			internal string pszName;

			// Token: 0x04000154 RID: 340
			[MarshalAs(21)]
			internal string pszSpec;
		}

		// Token: 0x02000055 RID: 85
		internal enum FDAP
		{
			// Token: 0x04000156 RID: 342
			FDAP_BOTTOM,
			// Token: 0x04000157 RID: 343
			FDAP_TOP
		}

		// Token: 0x02000056 RID: 86
		internal enum FDE_SHAREVIOLATION_RESPONSE
		{
			// Token: 0x04000159 RID: 345
			FDESVR_DEFAULT,
			// Token: 0x0400015A RID: 346
			FDESVR_ACCEPT,
			// Token: 0x0400015B RID: 347
			FDESVR_REFUSE
		}

		// Token: 0x02000057 RID: 87
		internal enum FDE_OVERWRITE_RESPONSE
		{
			// Token: 0x0400015D RID: 349
			FDEOR_DEFAULT,
			// Token: 0x0400015E RID: 350
			FDEOR_ACCEPT,
			// Token: 0x0400015F RID: 351
			FDEOR_REFUSE
		}

		// Token: 0x02000058 RID: 88
		internal enum SIATTRIBFLAGS
		{
			// Token: 0x04000161 RID: 353
			SIATTRIBFLAGS_AND = 1,
			// Token: 0x04000162 RID: 354
			SIATTRIBFLAGS_OR,
			// Token: 0x04000163 RID: 355
			SIATTRIBFLAGS_APPCOMPAT
		}

		// Token: 0x02000059 RID: 89
		internal enum SIGDN : uint
		{
			// Token: 0x04000165 RID: 357
			SIGDN_NORMALDISPLAY,
			// Token: 0x04000166 RID: 358
			SIGDN_PARENTRELATIVEPARSING = 2147581953U,
			// Token: 0x04000167 RID: 359
			SIGDN_DESKTOPABSOLUTEPARSING = 2147647488U,
			// Token: 0x04000168 RID: 360
			SIGDN_PARENTRELATIVEEDITING = 2147684353U,
			// Token: 0x04000169 RID: 361
			SIGDN_DESKTOPABSOLUTEEDITING = 2147794944U,
			// Token: 0x0400016A RID: 362
			SIGDN_FILESYSPATH = 2147844096U,
			// Token: 0x0400016B RID: 363
			SIGDN_URL = 2147909632U,
			// Token: 0x0400016C RID: 364
			SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553U,
			// Token: 0x0400016D RID: 365
			SIGDN_PARENTRELATIVE = 2148007937U
		}

		// Token: 0x0200005A RID: 90
		[Flags]
		internal enum FOS : uint
		{
			// Token: 0x0400016F RID: 367
			FOS_OVERWRITEPROMPT = 2U,
			// Token: 0x04000170 RID: 368
			FOS_STRICTFILETYPES = 4U,
			// Token: 0x04000171 RID: 369
			FOS_NOCHANGEDIR = 8U,
			// Token: 0x04000172 RID: 370
			FOS_PICKFOLDERS = 32U,
			// Token: 0x04000173 RID: 371
			FOS_FORCEFILESYSTEM = 64U,
			// Token: 0x04000174 RID: 372
			FOS_ALLNONSTORAGEITEMS = 128U,
			// Token: 0x04000175 RID: 373
			FOS_NOVALIDATE = 256U,
			// Token: 0x04000176 RID: 374
			FOS_ALLOWMULTISELECT = 512U,
			// Token: 0x04000177 RID: 375
			FOS_PATHMUSTEXIST = 2048U,
			// Token: 0x04000178 RID: 376
			FOS_FILEMUSTEXIST = 4096U,
			// Token: 0x04000179 RID: 377
			FOS_CREATEPROMPT = 8192U,
			// Token: 0x0400017A RID: 378
			FOS_SHAREAWARE = 16384U,
			// Token: 0x0400017B RID: 379
			FOS_NOREADONLYRETURN = 32768U,
			// Token: 0x0400017C RID: 380
			FOS_NOTESTFILECREATE = 65536U,
			// Token: 0x0400017D RID: 381
			FOS_HIDEMRUPLACES = 131072U,
			// Token: 0x0400017E RID: 382
			FOS_HIDEPINNEDPLACES = 262144U,
			// Token: 0x0400017F RID: 383
			FOS_NODEREFERENCELINKS = 1048576U,
			// Token: 0x04000180 RID: 384
			FOS_DONTADDTORECENT = 33554432U,
			// Token: 0x04000181 RID: 385
			FOS_FORCESHOWHIDDEN = 268435456U,
			// Token: 0x04000182 RID: 386
			FOS_DEFAULTNOMINIMODE = 536870912U
		}

		// Token: 0x0200005B RID: 91
		internal enum CDCONTROLSTATE
		{
			// Token: 0x04000184 RID: 388
			CDCS_INACTIVE,
			// Token: 0x04000185 RID: 389
			CDCS_ENABLED,
			// Token: 0x04000186 RID: 390
			CDCS_VISIBLE
		}

		// Token: 0x0200005C RID: 92
		internal enum FFFP_MODE
		{
			// Token: 0x04000188 RID: 392
			FFFP_EXACTMATCH,
			// Token: 0x04000189 RID: 393
			FFFP_NEARESTPARENTMATCH
		}

		// Token: 0x0200005D RID: 93
		[StructLayout(0, CharSet = 4, Pack = 4)]
		internal struct KNOWNFOLDER_DEFINITION
		{
			// Token: 0x0400018A RID: 394
			internal NativeMethods.KF_CATEGORY category;

			// Token: 0x0400018B RID: 395
			[MarshalAs(21)]
			internal string pszName;

			// Token: 0x0400018C RID: 396
			[MarshalAs(21)]
			internal string pszCreator;

			// Token: 0x0400018D RID: 397
			[MarshalAs(21)]
			internal string pszDescription;

			// Token: 0x0400018E RID: 398
			internal Guid fidParent;

			// Token: 0x0400018F RID: 399
			[MarshalAs(21)]
			internal string pszRelativePath;

			// Token: 0x04000190 RID: 400
			[MarshalAs(21)]
			internal string pszParsingName;

			// Token: 0x04000191 RID: 401
			[MarshalAs(21)]
			internal string pszToolTip;

			// Token: 0x04000192 RID: 402
			[MarshalAs(21)]
			internal string pszLocalizedName;

			// Token: 0x04000193 RID: 403
			[MarshalAs(21)]
			internal string pszIcon;

			// Token: 0x04000194 RID: 404
			[MarshalAs(21)]
			internal string pszSecurity;

			// Token: 0x04000195 RID: 405
			internal uint dwAttributes;

			// Token: 0x04000196 RID: 406
			internal NativeMethods.KF_DEFINITION_FLAGS kfdFlags;

			// Token: 0x04000197 RID: 407
			internal Guid ftidType;
		}

		// Token: 0x0200005E RID: 94
		internal enum KF_CATEGORY
		{
			// Token: 0x04000199 RID: 409
			KF_CATEGORY_VIRTUAL = 1,
			// Token: 0x0400019A RID: 410
			KF_CATEGORY_FIXED,
			// Token: 0x0400019B RID: 411
			KF_CATEGORY_COMMON,
			// Token: 0x0400019C RID: 412
			KF_CATEGORY_PERUSER
		}

		// Token: 0x0200005F RID: 95
		[Flags]
		internal enum KF_DEFINITION_FLAGS
		{
			// Token: 0x0400019E RID: 414
			KFDF_PERSONALIZE = 1,
			// Token: 0x0400019F RID: 415
			KFDF_LOCAL_REDIRECT_ONLY = 2,
			// Token: 0x040001A0 RID: 416
			KFDF_ROAMABLE = 4
		}

		// Token: 0x02000060 RID: 96
		[StructLayout(0, Pack = 4)]
		internal struct PROPERTYKEY
		{
			// Token: 0x040001A1 RID: 417
			internal Guid fmtid;

			// Token: 0x040001A2 RID: 418
			internal uint pid;
		}

		// Token: 0x02000061 RID: 97
		[Flags]
		public enum FormatMessageFlags
		{
			// Token: 0x040001A4 RID: 420
			FORMAT_MESSAGE_ALLOCATE_BUFFER = 256,
			// Token: 0x040001A5 RID: 421
			FORMAT_MESSAGE_IGNORE_INSERTS = 512,
			// Token: 0x040001A6 RID: 422
			FORMAT_MESSAGE_FROM_STRING = 1024,
			// Token: 0x040001A7 RID: 423
			FORMAT_MESSAGE_FROM_HMODULE = 2048,
			// Token: 0x040001A8 RID: 424
			FORMAT_MESSAGE_FROM_SYSTEM = 4096,
			// Token: 0x040001A9 RID: 425
			FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192
		}

		// Token: 0x02000062 RID: 98
		public enum HitTestResult
		{
			// Token: 0x040001AB RID: 427
			Error = -2,
			// Token: 0x040001AC RID: 428
			Transparent,
			// Token: 0x040001AD RID: 429
			Nowhere,
			// Token: 0x040001AE RID: 430
			Client,
			// Token: 0x040001AF RID: 431
			Caption,
			// Token: 0x040001B0 RID: 432
			SysMenu,
			// Token: 0x040001B1 RID: 433
			GrowBox,
			// Token: 0x040001B2 RID: 434
			Size = 4,
			// Token: 0x040001B3 RID: 435
			Menu,
			// Token: 0x040001B4 RID: 436
			HScroll,
			// Token: 0x040001B5 RID: 437
			VScroll,
			// Token: 0x040001B6 RID: 438
			MinButton,
			// Token: 0x040001B7 RID: 439
			MaxButton,
			// Token: 0x040001B8 RID: 440
			Left,
			// Token: 0x040001B9 RID: 441
			Right,
			// Token: 0x040001BA RID: 442
			Top,
			// Token: 0x040001BB RID: 443
			TopLeft,
			// Token: 0x040001BC RID: 444
			TopRight,
			// Token: 0x040001BD RID: 445
			Bottom,
			// Token: 0x040001BE RID: 446
			BottomLeft,
			// Token: 0x040001BF RID: 447
			BottomRight,
			// Token: 0x040001C0 RID: 448
			Border,
			// Token: 0x040001C1 RID: 449
			Reduce = 8,
			// Token: 0x040001C2 RID: 450
			Zoom,
			// Token: 0x040001C3 RID: 451
			SizeFirst,
			// Token: 0x040001C4 RID: 452
			SizeLast = 17,
			// Token: 0x040001C5 RID: 453
			Object = 19,
			// Token: 0x040001C6 RID: 454
			Close,
			// Token: 0x040001C7 RID: 455
			Help
		}

		// Token: 0x02000063 RID: 99
		public struct MARGINS
		{
			// Token: 0x060002F6 RID: 758 RVA: 0x0000A231 File Offset: 0x00008431
			public MARGINS(Padding value)
			{
				this.Left = value.Left;
				this.Right = value.Right;
				this.Top = value.Top;
				this.Bottom = value.Bottom;
			}

			// Token: 0x040001C8 RID: 456
			public int Left;

			// Token: 0x040001C9 RID: 457
			public int Right;

			// Token: 0x040001CA RID: 458
			public int Top;

			// Token: 0x040001CB RID: 459
			public int Bottom;
		}

		// Token: 0x02000064 RID: 100
		public struct DTTOPTS
		{
			// Token: 0x040001CC RID: 460
			public int dwSize;

			// Token: 0x040001CD RID: 461
			[MarshalAs(8)]
			public NativeMethods.DrawThemeTextFlags dwFlags;

			// Token: 0x040001CE RID: 462
			public int crText;

			// Token: 0x040001CF RID: 463
			public int crBorder;

			// Token: 0x040001D0 RID: 464
			public int crShadow;

			// Token: 0x040001D1 RID: 465
			public int iTextShadowType;

			// Token: 0x040001D2 RID: 466
			public Point ptShadowOffset;

			// Token: 0x040001D3 RID: 467
			public int iBorderSize;

			// Token: 0x040001D4 RID: 468
			public int iFontPropId;

			// Token: 0x040001D5 RID: 469
			public int iColorPropId;

			// Token: 0x040001D6 RID: 470
			public int iStateId;

			// Token: 0x040001D7 RID: 471
			public bool fApplyOverlay;

			// Token: 0x040001D8 RID: 472
			public int iGlowSize;

			// Token: 0x040001D9 RID: 473
			public int pfnDrawTextCallback;

			// Token: 0x040001DA RID: 474
			public IntPtr lParam;
		}

		// Token: 0x02000065 RID: 101
		[Flags]
		public enum DrawThemeTextFlags
		{
			// Token: 0x040001DC RID: 476
			TextColor = 1,
			// Token: 0x040001DD RID: 477
			BorderColor = 2,
			// Token: 0x040001DE RID: 478
			ShadowColor = 4,
			// Token: 0x040001DF RID: 479
			ShadowType = 8,
			// Token: 0x040001E0 RID: 480
			ShadowOffset = 16,
			// Token: 0x040001E1 RID: 481
			BorderSize = 32,
			// Token: 0x040001E2 RID: 482
			FontProp = 64,
			// Token: 0x040001E3 RID: 483
			ColorProp = 128,
			// Token: 0x040001E4 RID: 484
			StateId = 256,
			// Token: 0x040001E5 RID: 485
			CalcRect = 512,
			// Token: 0x040001E6 RID: 486
			ApplyOverlay = 1024,
			// Token: 0x040001E7 RID: 487
			GlowSize = 2048,
			// Token: 0x040001E8 RID: 488
			Callback = 4096,
			// Token: 0x040001E9 RID: 489
			Composited = 8192
		}

		// Token: 0x02000066 RID: 102
		[StructLayout(0)]
		public class BITMAPINFO
		{
			// Token: 0x040001EA RID: 490
			public int biSize;

			// Token: 0x040001EB RID: 491
			public int biWidth;

			// Token: 0x040001EC RID: 492
			public int biHeight;

			// Token: 0x040001ED RID: 493
			public short biPlanes;

			// Token: 0x040001EE RID: 494
			public short biBitCount;

			// Token: 0x040001EF RID: 495
			public int biCompression;

			// Token: 0x040001F0 RID: 496
			public int biSizeImage;

			// Token: 0x040001F1 RID: 497
			public int biXPelsPerMeter;

			// Token: 0x040001F2 RID: 498
			public int biYPelsPerMeter;

			// Token: 0x040001F3 RID: 499
			public int biClrUsed;

			// Token: 0x040001F4 RID: 500
			public int biClrImportant;

			// Token: 0x040001F5 RID: 501
			public byte bmiColors_rgbBlue;

			// Token: 0x040001F6 RID: 502
			public byte bmiColors_rgbGreen;

			// Token: 0x040001F7 RID: 503
			public byte bmiColors_rgbRed;

			// Token: 0x040001F8 RID: 504
			public byte bmiColors_rgbReserved;
		}

		// Token: 0x02000067 RID: 103
		public struct RECT
		{
			// Token: 0x060002F8 RID: 760 RVA: 0x0000A271 File Offset: 0x00008471
			public RECT(int left, int top, int right, int bottom)
			{
				this.Left = left;
				this.Top = top;
				this.Right = right;
				this.Bottom = bottom;
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000A291 File Offset: 0x00008491
			public RECT(Rectangle rectangle)
			{
				this.Left = rectangle.X;
				this.Top = rectangle.Y;
				this.Right = rectangle.Right;
				this.Bottom = rectangle.Bottom;
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000A2C8 File Offset: 0x000084C8
			public override string ToString()
			{
				return string.Concat(new object[] { "Left: ", this.Left, ", Top: ", this.Top, ", Right: ", this.Right, ", Bottom: ", this.Bottom });
			}

			// Token: 0x040001F9 RID: 505
			public int Left;

			// Token: 0x040001FA RID: 506
			public int Top;

			// Token: 0x040001FB RID: 507
			public int Right;

			// Token: 0x040001FC RID: 508
			public int Bottom;
		}

		// Token: 0x02000068 RID: 104
		[Flags]
		public enum CREDUI_FLAGS
		{
			// Token: 0x040001FE RID: 510
			INCORRECT_PASSWORD = 1,
			// Token: 0x040001FF RID: 511
			DO_NOT_PERSIST = 2,
			// Token: 0x04000200 RID: 512
			REQUEST_ADMINISTRATOR = 4,
			// Token: 0x04000201 RID: 513
			EXCLUDE_CERTIFICATES = 8,
			// Token: 0x04000202 RID: 514
			REQUIRE_CERTIFICATE = 16,
			// Token: 0x04000203 RID: 515
			SHOW_SAVE_CHECK_BOX = 64,
			// Token: 0x04000204 RID: 516
			ALWAYS_SHOW_UI = 128,
			// Token: 0x04000205 RID: 517
			REQUIRE_SMARTCARD = 256,
			// Token: 0x04000206 RID: 518
			PASSWORD_ONLY_OK = 512,
			// Token: 0x04000207 RID: 519
			VALIDATE_USERNAME = 1024,
			// Token: 0x04000208 RID: 520
			COMPLETE_USERNAME = 2048,
			// Token: 0x04000209 RID: 521
			PERSIST = 4096,
			// Token: 0x0400020A RID: 522
			SERVER_CREDENTIAL = 16384,
			// Token: 0x0400020B RID: 523
			EXPECT_CONFIRMATION = 131072,
			// Token: 0x0400020C RID: 524
			GENERIC_CREDENTIALS = 262144,
			// Token: 0x0400020D RID: 525
			USERNAME_TARGET_CREDENTIALS = 524288,
			// Token: 0x0400020E RID: 526
			KEEP_USERNAME = 1048576
		}

		// Token: 0x02000069 RID: 105
		[Flags]
		public enum CredUIWinFlags
		{
			// Token: 0x04000210 RID: 528
			Generic = 1,
			// Token: 0x04000211 RID: 529
			Checkbox = 2,
			// Token: 0x04000212 RID: 530
			AutoPackageOnly = 16,
			// Token: 0x04000213 RID: 531
			InCredOnly = 32,
			// Token: 0x04000214 RID: 532
			EnumerateAdmins = 256,
			// Token: 0x04000215 RID: 533
			EnumerateCurrentUser = 512,
			// Token: 0x04000216 RID: 534
			SecurePrompt = 4096,
			// Token: 0x04000217 RID: 535
			Pack32Wow = 268435456
		}

		// Token: 0x0200006A RID: 106
		internal enum CredUIReturnCodes
		{
			// Token: 0x04000219 RID: 537
			NO_ERROR,
			// Token: 0x0400021A RID: 538
			ERROR_CANCELLED = 1223,
			// Token: 0x0400021B RID: 539
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			// Token: 0x0400021C RID: 540
			ERROR_NOT_FOUND = 1168,
			// Token: 0x0400021D RID: 541
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			// Token: 0x0400021E RID: 542
			ERROR_INSUFFICIENT_BUFFER = 122,
			// Token: 0x0400021F RID: 543
			ERROR_INVALID_PARAMETER = 87,
			// Token: 0x04000220 RID: 544
			ERROR_INVALID_FLAGS = 1004
		}

		// Token: 0x0200006B RID: 107
		internal enum CredTypes
		{
			// Token: 0x04000222 RID: 546
			CRED_TYPE_GENERIC = 1,
			// Token: 0x04000223 RID: 547
			CRED_TYPE_DOMAIN_PASSWORD,
			// Token: 0x04000224 RID: 548
			CRED_TYPE_DOMAIN_CERTIFICATE,
			// Token: 0x04000225 RID: 549
			CRED_TYPE_DOMAIN_VISIBLE_PASSWORD
		}

		// Token: 0x0200006C RID: 108
		internal enum CredPersist
		{
			// Token: 0x04000227 RID: 551
			Session = 1,
			// Token: 0x04000228 RID: 552
			LocalMachine,
			// Token: 0x04000229 RID: 553
			Enterprise
		}

		// Token: 0x0200006D RID: 109
		internal struct CREDUI_INFO
		{
			// Token: 0x0400022A RID: 554
			public int cbSize;

			// Token: 0x0400022B RID: 555
			public IntPtr hwndParent;

			// Token: 0x0400022C RID: 556
			[MarshalAs(21)]
			public string pszMessageText;

			// Token: 0x0400022D RID: 557
			[MarshalAs(21)]
			public string pszCaptionText;

			// Token: 0x0400022E RID: 558
			public IntPtr hbmBanner;
		}

		// Token: 0x0200006E RID: 110
		public struct CREDENTIAL
		{
			// Token: 0x0400022F RID: 559
			public int Flags;

			// Token: 0x04000230 RID: 560
			public NativeMethods.CredTypes Type;

			// Token: 0x04000231 RID: 561
			[MarshalAs(21)]
			public string TargetName;

			// Token: 0x04000232 RID: 562
			[MarshalAs(21)]
			public string Comment;

			// Token: 0x04000233 RID: 563
			public long LastWritten;

			// Token: 0x04000234 RID: 564
			public uint CredentialBlobSize;

			// Token: 0x04000235 RID: 565
			public IntPtr CredentialBlob;

			// Token: 0x04000236 RID: 566
			[MarshalAs(8)]
			public NativeMethods.CredPersist Persist;

			// Token: 0x04000237 RID: 567
			public int AttributeCount;

			// Token: 0x04000238 RID: 568
			public IntPtr Attributes;

			// Token: 0x04000239 RID: 569
			[MarshalAs(21)]
			public string TargetAlias;

			// Token: 0x0400023A RID: 570
			[MarshalAs(21)]
			public string UserName;
		}
	}
}
