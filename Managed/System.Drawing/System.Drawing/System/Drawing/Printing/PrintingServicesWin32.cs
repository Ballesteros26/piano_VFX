using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing.Printing
{
	// Token: 0x020000E3 RID: 227
	internal class PrintingServicesWin32 : PrintingServices
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x00018634 File Offset: 0x00016834
		internal PrintingServicesWin32()
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00019988 File Offset: 0x00017B88
		internal override bool IsPrinterValid(string printer)
		{
			if ((printer == null) | (printer == string.Empty))
			{
				return false;
			}
			int num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, IntPtr.Zero, printer, IntPtr.Zero, IntPtr.Zero, 0);
			this.is_printer_valid = num > 0;
			return this.is_printer_valid;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000199D8 File Offset: 0x00017BD8
		internal override void LoadPrinterSettings(string printer, PrinterSettings settings)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			settings.maximum_copies = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COPIES, IntPtr.Zero, IntPtr.Zero);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_DUPLEX, IntPtr.Zero, IntPtr.Zero);
			settings.can_duplex = num == 1;
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COLORDEVICE, IntPtr.Zero, IntPtr.Zero);
			settings.supports_color = num == 1;
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ORIENTATION, IntPtr.Zero, IntPtr.Zero);
			if (num != -1)
			{
				settings.landscape_angle = num;
			}
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = PrintingServicesWin32.Win32CreateIC(null, printer, null, IntPtr.Zero);
			num = PrintingServicesWin32.Win32GetDeviceCaps(intPtr2, 2);
			settings.is_plotter = num == 0;
			PrintingServicesWin32.Win32DeleteDC(intPtr2);
			try
			{
				PrintingServicesWin32.Win32OpenPrinter(printer, out zero, IntPtr.Zero);
				num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, IntPtr.Zero, IntPtr.Zero, 0);
				if (num >= 0)
				{
					intPtr = Marshal.AllocHGlobal(num);
					num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, intPtr, IntPtr.Zero, 2);
					PrintingServicesWin32.DEVMODE devmode = (PrintingServicesWin32.DEVMODE)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesWin32.DEVMODE));
					this.LoadPrinterPaperSizes(printer, settings);
					foreach (object obj in settings.PaperSizes)
					{
						PaperSize paperSize = (PaperSize)obj;
						if (paperSize.Kind == (PaperKind)devmode.dmPaperSize)
						{
							settings.DefaultPageSettings.PaperSize = paperSize;
							break;
						}
					}
					this.LoadPrinterPaperSources(printer, settings);
					foreach (object obj2 in settings.PaperSources)
					{
						PaperSource paperSource = (PaperSource)obj2;
						if (paperSource.Kind == (PaperSourceKind)devmode.dmDefaultSource)
						{
							settings.DefaultPageSettings.PaperSource = paperSource;
							break;
						}
					}
				}
			}
			finally
			{
				PrintingServicesWin32.Win32ClosePrinter(zero);
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00019C24 File Offset: 0x00017E24
		internal override void LoadPrinterResolutions(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			settings.PrinterResolutions.Clear();
			base.LoadDefaultResolutions(settings.PrinterResolutions);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			IntPtr intPtr2;
			intPtr = (intPtr2 = Marshal.AllocHGlobal(num * 2 * Marshal.SizeOf<IntPtr>(intPtr)));
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, intPtr, IntPtr.Zero);
			if (num != -1)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = Marshal.ReadInt32(intPtr2);
					intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf<int>(num2));
					int num3 = Marshal.ReadInt32(intPtr2);
					intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf<int>(num3));
					settings.PrinterResolutions.Add(new PrinterResolution(PrinterResolutionKind.Custom, num2, num3));
				}
			}
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00019CF4 File Offset: 0x00017EF4
		private void LoadPrinterPaperSizes(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			if (settings.PaperSizes == null)
			{
				settings.paper_sizes = new PrinterSettings.PaperSizeCollection(new PaperSize[0]);
			}
			else
			{
				settings.PaperSizes.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr intPtr4;
				intPtr2 = (intPtr4 = Marshal.AllocHGlobal(num * 2 * 4));
				IntPtr intPtr5;
				intPtr = (intPtr5 = Marshal.AllocHGlobal(num * 64 * 2));
				IntPtr intPtr6;
				intPtr3 = (intPtr6 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, intPtr2, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERS, intPtr3, IntPtr.Zero);
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERNAMES, intPtr, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						int num3 = Marshal.ReadInt32(intPtr4, i * 8);
						int num4 = Marshal.ReadInt32(intPtr4, i * 8 + 4);
						num3 = PrinterUnitConvert.Convert(num3, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						num4 = PrinterUnitConvert.Convert(num4, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						string text = Marshal.PtrToStringUni(intPtr5);
						intPtr5 = new IntPtr(intPtr5.ToInt64() + 128L);
						PaperKind paperKind = (PaperKind)Marshal.ReadInt16(intPtr6);
						intPtr6 = new IntPtr(intPtr6.ToInt64() + 2L);
						PaperSize paperSize = new PaperSize(text, num3, num4);
						paperSize.RawKind = (int)paperKind;
						settings.PaperSizes.Add(paperSize);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00019EB4 File Offset: 0x000180B4
		internal static bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			PrintingServicesWin32.DOCINFO docinfo = default(PrintingServicesWin32.DOCINFO);
			docinfo.cbSize = Marshal.SizeOf<PrintingServicesWin32.DOCINFO>(docinfo);
			docinfo.lpszDocName = Marshal.StringToHGlobalUni(doc_name);
			docinfo.lpszOutput = IntPtr.Zero;
			docinfo.lpszDatatype = IntPtr.Zero;
			docinfo.fwType = 0;
			int num = PrintingServicesWin32.Win32StartDoc(gr.Hdc, ref docinfo);
			Marshal.FreeHGlobal(docinfo.lpszDocName);
			return num > 0;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00019F24 File Offset: 0x00018124
		private void LoadPrinterPaperSources(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (settings.PaperSources == null)
			{
				settings.paper_sources = new PrinterSettings.PaperSourceCollection(new PaperSource[0]);
			}
			else
			{
				settings.PaperSources.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr intPtr3;
				intPtr = (intPtr3 = Marshal.AllocHGlobal(num * 2 * 24));
				IntPtr intPtr4;
				intPtr2 = (intPtr4 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, intPtr, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINS, intPtr2, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						string text = Marshal.PtrToStringUni(intPtr3);
						PaperSourceKind paperSourceKind = (PaperSourceKind)Marshal.ReadInt16(intPtr4);
						settings.PaperSources.Add(new PaperSource(paperSourceKind, text));
						intPtr3 = new IntPtr(intPtr3.ToInt64() + 48L);
						intPtr4 = new IntPtr(intPtr4.ToInt64() + 2L);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001A058 File Offset: 0x00018258
		internal static bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32StartPage(gr.Hdc) > 0;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001A06B File Offset: 0x0001826B
		internal static bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32EndPage(gr.Hdc) > 0;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001A07E File Offset: 0x0001827E
		internal static bool EndDoc(GraphicsPrinter gr)
		{
			int num = PrintingServicesWin32.Win32EndDoc(gr.Hdc);
			PrintingServicesWin32.Win32DeleteDC(gr.Hdc);
			gr.Graphics.Dispose();
			return num > 0;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001A0A8 File Offset: 0x000182A8
		internal static IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			IntPtr zero = IntPtr.Zero;
			return PrintingServicesWin32.Win32CreateDC(null, settings.PrinterName, null, IntPtr.Zero);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0001A0C4 File Offset: 0x000182C4
		internal override string DefaultPrinter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				if (PrintingServicesWin32.Win32GetDefaultPrinter(stringBuilder, ref capacity) > 0 && this.IsPrinterValid(stringBuilder.ToString()))
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001A108 File Offset: 0x00018308
		internal static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				PrinterSettings.StringCollection stringCollection = new PrinterSettings.StringCollection(new string[0]);
				uint num = 0U;
				uint num2 = 0U;
				PrintingServicesWin32.Win32EnumPrinters(6, null, 2U, IntPtr.Zero, 0U, ref num, ref num2);
				if (num <= 0U)
				{
					return stringCollection;
				}
				IntPtr intPtr2;
				IntPtr intPtr = (intPtr2 = Marshal.AllocHGlobal((int)num));
				try
				{
					PrintingServicesWin32.Win32EnumPrinters(6, null, 2U, intPtr, num, ref num, ref num2);
					int num3 = 0;
					while ((long)num3 < (long)((ulong)num2))
					{
						PrintingServicesWin32.PRINTER_INFO printer_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesWin32.PRINTER_INFO));
						string text = Marshal.PtrToStringUni(printer_INFO.pPrinterName);
						stringCollection.Add(text);
						intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)Marshal.SizeOf<PrintingServicesWin32.PRINTER_INFO>(printer_INFO));
						num3++;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return stringCollection;
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001A1CC File Offset: 0x000183CC
		internal override void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			PrintingServicesWin32.PRINTER_INFO printer_INFO = default(PrintingServicesWin32.PRINTER_INFO);
			int num = 0;
			IntPtr intPtr;
			PrintingServicesWin32.Win32OpenPrinter(printer, out intPtr, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, IntPtr.Zero, 0, ref num);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, intPtr2, num, ref num);
			printer_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesWin32.PRINTER_INFO));
			Marshal.FreeHGlobal(intPtr2);
			port = Marshal.PtrToStringUni(printer_INFO.pPortName);
			comment = Marshal.PtrToStringUni(printer_INFO.pComment);
			type = Marshal.PtrToStringUni(printer_INFO.pDriverName);
			status = this.GetPrinterStatusMsg(printer_INFO.Status);
			PrintingServicesWin32.Win32ClosePrinter(intPtr);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001A27C File Offset: 0x0001847C
		private string GetPrinterStatusMsg(uint status)
		{
			string text = string.Empty;
			if (status == 0U)
			{
				return "Ready";
			}
			if ((status & 1U) != 0U)
			{
				text += "Paused; ";
			}
			if ((status & 2U) != 0U)
			{
				text += "Error; ";
			}
			if ((status & 4U) != 0U)
			{
				text += "Pending deletion; ";
			}
			if ((status & 8U) != 0U)
			{
				text += "Paper jam; ";
			}
			if ((status & 16U) != 0U)
			{
				text += "Paper out; ";
			}
			if ((status & 32U) != 0U)
			{
				text += "Manual feed; ";
			}
			if ((status & 64U) != 0U)
			{
				text += "Paper problem; ";
			}
			if ((status & 128U) != 0U)
			{
				text += "Offline; ";
			}
			if ((status & 256U) != 0U)
			{
				text += "I/O active; ";
			}
			if ((status & 512U) != 0U)
			{
				text += "Busy; ";
			}
			if ((status & 1024U) != 0U)
			{
				text += "Printing; ";
			}
			if ((status & 2048U) != 0U)
			{
				text += "Output bin full; ";
			}
			if ((status & 4096U) != 0U)
			{
				text += "Not available; ";
			}
			if ((status & 8192U) != 0U)
			{
				text += "Waiting; ";
			}
			if ((status & 16384U) != 0U)
			{
				text += "Processing; ";
			}
			if ((status & 32768U) != 0U)
			{
				text += "Initializing; ";
			}
			if ((status & 65536U) != 0U)
			{
				text += "Warming up; ";
			}
			if ((status & 131072U) != 0U)
			{
				text += "Toner low; ";
			}
			if ((status & 262144U) != 0U)
			{
				text += "No toner; ";
			}
			if ((status & 524288U) != 0U)
			{
				text += "Page punt; ";
			}
			if ((status & 1048576U) != 0U)
			{
				text += "User intervention; ";
			}
			if ((status & 2097152U) != 0U)
			{
				text += "Out of memory; ";
			}
			if ((status & 4194304U) != 0U)
			{
				text += "Door open; ";
			}
			if ((status & 8388608U) != 0U)
			{
				text += "Server unkown; ";
			}
			if ((status & 16777216U) != 0U)
			{
				text += "Power save; ";
			}
			return text;
		}

		// Token: 0x06000BBC RID: 3004
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "OpenPrinter", SetLastError = true)]
		private static extern int Win32OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

		// Token: 0x06000BBD RID: 3005
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetPrinter", SetLastError = true)]
		private static extern int Win32GetPrinter(IntPtr hPrinter, int level, IntPtr dwBuf, int size, ref int dwNeeded);

		// Token: 0x06000BBE RID: 3006
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "ClosePrinter", SetLastError = true)]
		private static extern int Win32ClosePrinter(IntPtr hPrinter);

		// Token: 0x06000BBF RID: 3007
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DeviceCapabilities", SetLastError = true)]
		private static extern int Win32DeviceCapabilities(string device, string port, PrintingServicesWin32.DCCapabilities cap, IntPtr outputBuffer, IntPtr deviceMode);

		// Token: 0x06000BC0 RID: 3008
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "EnumPrinters", SetLastError = true)]
		private static extern int Win32EnumPrinters(int Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);

		// Token: 0x06000BC1 RID: 3009
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetDefaultPrinter", SetLastError = true)]
		private static extern int Win32GetDefaultPrinter(StringBuilder buffer, ref int bufferSize);

		// Token: 0x06000BC2 RID: 3010
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DocumentProperties", SetLastError = true)]
		private static extern int Win32DocumentProperties(IntPtr hwnd, IntPtr hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		// Token: 0x06000BC3 RID: 3011
		[DllImport("gdi32.dll", EntryPoint = "CreateDC")]
		private static extern IntPtr Win32CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		// Token: 0x06000BC4 RID: 3012
		[DllImport("gdi32.dll", EntryPoint = "CreateIC")]
		private static extern IntPtr Win32CreateIC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		// Token: 0x06000BC5 RID: 3013
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, EntryPoint = "StartDoc")]
		private static extern int Win32StartDoc(IntPtr hdc, [In] ref PrintingServicesWin32.DOCINFO lpdi);

		// Token: 0x06000BC6 RID: 3014
		[DllImport("gdi32.dll", EntryPoint = "StartPage")]
		private static extern int Win32StartPage(IntPtr hDC);

		// Token: 0x06000BC7 RID: 3015
		[DllImport("gdi32.dll", EntryPoint = "EndPage")]
		private static extern int Win32EndPage(IntPtr hdc);

		// Token: 0x06000BC8 RID: 3016
		[DllImport("gdi32.dll", EntryPoint = "EndDoc")]
		private static extern int Win32EndDoc(IntPtr hdc);

		// Token: 0x06000BC9 RID: 3017
		[DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
		public static extern IntPtr Win32DeleteDC(IntPtr hDc);

		// Token: 0x06000BCA RID: 3018
		[DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
		public static extern int Win32GetDeviceCaps(IntPtr hDc, int index);

		// Token: 0x04000787 RID: 1927
		private bool is_printer_valid;

		// Token: 0x020000E4 RID: 228
		internal struct PRINTER_INFO
		{
			// Token: 0x04000788 RID: 1928
			public IntPtr pServerName;

			// Token: 0x04000789 RID: 1929
			public IntPtr pPrinterName;

			// Token: 0x0400078A RID: 1930
			public IntPtr pShareName;

			// Token: 0x0400078B RID: 1931
			public IntPtr pPortName;

			// Token: 0x0400078C RID: 1932
			public IntPtr pDriverName;

			// Token: 0x0400078D RID: 1933
			public IntPtr pComment;

			// Token: 0x0400078E RID: 1934
			public IntPtr pLocation;

			// Token: 0x0400078F RID: 1935
			public IntPtr pDevMode;

			// Token: 0x04000790 RID: 1936
			public IntPtr pSepFile;

			// Token: 0x04000791 RID: 1937
			public IntPtr pPrintProcessor;

			// Token: 0x04000792 RID: 1938
			public IntPtr pDatatype;

			// Token: 0x04000793 RID: 1939
			public IntPtr pParameters;

			// Token: 0x04000794 RID: 1940
			public IntPtr pSecurityDescriptor;

			// Token: 0x04000795 RID: 1941
			public uint Attributes;

			// Token: 0x04000796 RID: 1942
			public uint Priority;

			// Token: 0x04000797 RID: 1943
			public uint DefaultPriority;

			// Token: 0x04000798 RID: 1944
			public uint StartTime;

			// Token: 0x04000799 RID: 1945
			public uint UntilTime;

			// Token: 0x0400079A RID: 1946
			public uint Status;

			// Token: 0x0400079B RID: 1947
			public uint cJobs;

			// Token: 0x0400079C RID: 1948
			public uint AveragePPM;
		}

		// Token: 0x020000E5 RID: 229
		internal struct DOCINFO
		{
			// Token: 0x0400079D RID: 1949
			public int cbSize;

			// Token: 0x0400079E RID: 1950
			public IntPtr lpszDocName;

			// Token: 0x0400079F RID: 1951
			public IntPtr lpszOutput;

			// Token: 0x040007A0 RID: 1952
			public IntPtr lpszDatatype;

			// Token: 0x040007A1 RID: 1953
			public int fwType;
		}

		// Token: 0x020000E6 RID: 230
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DEVMODE
		{
			// Token: 0x040007A2 RID: 1954
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x040007A3 RID: 1955
			public short dmSpecVersion;

			// Token: 0x040007A4 RID: 1956
			public short dmDriverVersion;

			// Token: 0x040007A5 RID: 1957
			public short dmSize;

			// Token: 0x040007A6 RID: 1958
			public short dmDriverExtra;

			// Token: 0x040007A7 RID: 1959
			public int dmFields;

			// Token: 0x040007A8 RID: 1960
			public short dmOrientation;

			// Token: 0x040007A9 RID: 1961
			public short dmPaperSize;

			// Token: 0x040007AA RID: 1962
			public short dmPaperLength;

			// Token: 0x040007AB RID: 1963
			public short dmPaperWidth;

			// Token: 0x040007AC RID: 1964
			public short dmScale;

			// Token: 0x040007AD RID: 1965
			public short dmCopies;

			// Token: 0x040007AE RID: 1966
			public short dmDefaultSource;

			// Token: 0x040007AF RID: 1967
			public short dmPrintQuality;

			// Token: 0x040007B0 RID: 1968
			public short dmColor;

			// Token: 0x040007B1 RID: 1969
			public short dmDuplex;

			// Token: 0x040007B2 RID: 1970
			public short dmYResolution;

			// Token: 0x040007B3 RID: 1971
			public short dmTTOption;

			// Token: 0x040007B4 RID: 1972
			public short dmCollate;

			// Token: 0x040007B5 RID: 1973
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x040007B6 RID: 1974
			public short dmLogPixels;

			// Token: 0x040007B7 RID: 1975
			public short dmBitsPerPel;

			// Token: 0x040007B8 RID: 1976
			public int dmPelsWidth;

			// Token: 0x040007B9 RID: 1977
			public int dmPelsHeight;

			// Token: 0x040007BA RID: 1978
			public int dmDisplayFlags;

			// Token: 0x040007BB RID: 1979
			public int dmDisplayFrequency;

			// Token: 0x040007BC RID: 1980
			public int dmICMMethod;

			// Token: 0x040007BD RID: 1981
			public int dmICMIntent;

			// Token: 0x040007BE RID: 1982
			public int dmMediaType;

			// Token: 0x040007BF RID: 1983
			public int dmDitherType;

			// Token: 0x040007C0 RID: 1984
			public int dmReserved1;

			// Token: 0x040007C1 RID: 1985
			public int dmReserved2;

			// Token: 0x040007C2 RID: 1986
			public int dmPanningWidth;

			// Token: 0x040007C3 RID: 1987
			public int dmPanningHeight;
		}

		// Token: 0x020000E7 RID: 231
		internal enum DCCapabilities : short
		{
			// Token: 0x040007C5 RID: 1989
			DC_FIELDS = 1,
			// Token: 0x040007C6 RID: 1990
			DC_PAPERS,
			// Token: 0x040007C7 RID: 1991
			DC_PAPERSIZE,
			// Token: 0x040007C8 RID: 1992
			DC_MINEXTENT,
			// Token: 0x040007C9 RID: 1993
			DC_MAXEXTENT,
			// Token: 0x040007CA RID: 1994
			DC_BINS,
			// Token: 0x040007CB RID: 1995
			DC_DUPLEX,
			// Token: 0x040007CC RID: 1996
			DC_SIZE,
			// Token: 0x040007CD RID: 1997
			DC_EXTRA,
			// Token: 0x040007CE RID: 1998
			DC_VERSION,
			// Token: 0x040007CF RID: 1999
			DC_DRIVER,
			// Token: 0x040007D0 RID: 2000
			DC_BINNAMES,
			// Token: 0x040007D1 RID: 2001
			DC_ENUMRESOLUTIONS,
			// Token: 0x040007D2 RID: 2002
			DC_FILEDEPENDENCIES,
			// Token: 0x040007D3 RID: 2003
			DC_TRUETYPE,
			// Token: 0x040007D4 RID: 2004
			DC_PAPERNAMES,
			// Token: 0x040007D5 RID: 2005
			DC_ORIENTATION,
			// Token: 0x040007D6 RID: 2006
			DC_COPIES,
			// Token: 0x040007D7 RID: 2007
			DC_BINADJUST,
			// Token: 0x040007D8 RID: 2008
			DC_EMF_COMPLIANT,
			// Token: 0x040007D9 RID: 2009
			DC_DATATYPE_PRODUCED,
			// Token: 0x040007DA RID: 2010
			DC_COLLATE,
			// Token: 0x040007DB RID: 2011
			DC_MANUFACTURER,
			// Token: 0x040007DC RID: 2012
			DC_MODEL,
			// Token: 0x040007DD RID: 2013
			DC_PERSONALITY,
			// Token: 0x040007DE RID: 2014
			DC_PRINTRATE,
			// Token: 0x040007DF RID: 2015
			DC_PRINTRATEUNIT,
			// Token: 0x040007E0 RID: 2016
			DC_PRINTERMEM,
			// Token: 0x040007E1 RID: 2017
			DC_MEDIAREADY,
			// Token: 0x040007E2 RID: 2018
			DC_STAPLE,
			// Token: 0x040007E3 RID: 2019
			DC_PRINTRATEPPM,
			// Token: 0x040007E4 RID: 2020
			DC_COLORDEVICE,
			// Token: 0x040007E5 RID: 2021
			DC_NUP
		}

		// Token: 0x020000E8 RID: 232
		[Flags]
		internal enum PrinterStatus : uint
		{
			// Token: 0x040007E7 RID: 2023
			PS_PAUSED = 1U,
			// Token: 0x040007E8 RID: 2024
			PS_ERROR = 2U,
			// Token: 0x040007E9 RID: 2025
			PS_PENDING_DELETION = 4U,
			// Token: 0x040007EA RID: 2026
			PS_PAPER_JAM = 8U,
			// Token: 0x040007EB RID: 2027
			PS_PAPER_OUT = 16U,
			// Token: 0x040007EC RID: 2028
			PS_MANUAL_FEED = 32U,
			// Token: 0x040007ED RID: 2029
			PS_PAPER_PROBLEM = 64U,
			// Token: 0x040007EE RID: 2030
			PS_OFFLINE = 128U,
			// Token: 0x040007EF RID: 2031
			PS_IO_ACTIVE = 256U,
			// Token: 0x040007F0 RID: 2032
			PS_BUSY = 512U,
			// Token: 0x040007F1 RID: 2033
			PS_PRINTING = 1024U,
			// Token: 0x040007F2 RID: 2034
			PS_OUTPUT_BIN_FULL = 2048U,
			// Token: 0x040007F3 RID: 2035
			PS_NOT_AVAILABLE = 4096U,
			// Token: 0x040007F4 RID: 2036
			PS_WAITING = 8192U,
			// Token: 0x040007F5 RID: 2037
			PS_PROCESSING = 16384U,
			// Token: 0x040007F6 RID: 2038
			PS_INITIALIZING = 32768U,
			// Token: 0x040007F7 RID: 2039
			PS_WARMING_UP = 65536U,
			// Token: 0x040007F8 RID: 2040
			PS_TONER_LOW = 131072U,
			// Token: 0x040007F9 RID: 2041
			PS_NO_TONER = 262144U,
			// Token: 0x040007FA RID: 2042
			PS_PAGE_PUNT = 524288U,
			// Token: 0x040007FB RID: 2043
			PS_USER_INTERVENTION = 1048576U,
			// Token: 0x040007FC RID: 2044
			PS_OUT_OF_MEMORY = 2097152U,
			// Token: 0x040007FD RID: 2045
			PS_DOOR_OPEN = 4194304U,
			// Token: 0x040007FE RID: 2046
			PS_SERVER_UNKNOWN = 8388608U,
			// Token: 0x040007FF RID: 2047
			PS_POWER_SAVE = 16777216U
		}

		// Token: 0x020000E9 RID: 233
		internal enum DevCapabilities
		{
			// Token: 0x04000801 RID: 2049
			TECHNOLOGY = 2
		}

		// Token: 0x020000EA RID: 234
		internal enum PrinterType
		{
			// Token: 0x04000803 RID: 2051
			DT_PLOTTER,
			// Token: 0x04000804 RID: 2052
			DT_RASDIPLAY,
			// Token: 0x04000805 RID: 2053
			DT_RASPRINTER,
			// Token: 0x04000806 RID: 2054
			DT_RASCAMERA,
			// Token: 0x04000807 RID: 2055
			DT_CHARSTREAM,
			// Token: 0x04000808 RID: 2056
			DT_METAFILE,
			// Token: 0x04000809 RID: 2057
			DT_DISPFILE
		}

		// Token: 0x020000EB RID: 235
		[Flags]
		internal enum EnumPrinters : uint
		{
			// Token: 0x0400080B RID: 2059
			PRINTER_ENUM_DEFAULT = 1U,
			// Token: 0x0400080C RID: 2060
			PRINTER_ENUM_LOCAL = 2U,
			// Token: 0x0400080D RID: 2061
			PRINTER_ENUM_CONNECTIONS = 4U,
			// Token: 0x0400080E RID: 2062
			PRINTER_ENUM_FAVORITE = 4U,
			// Token: 0x0400080F RID: 2063
			PRINTER_ENUM_NAME = 8U,
			// Token: 0x04000810 RID: 2064
			PRINTER_ENUM_REMOTE = 16U,
			// Token: 0x04000811 RID: 2065
			PRINTER_ENUM_SHARED = 32U,
			// Token: 0x04000812 RID: 2066
			PRINTER_ENUM_NETWORK = 64U
		}
	}
}
