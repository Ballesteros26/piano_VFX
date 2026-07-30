using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D6 RID: 214
	internal class SysPrn
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0001858F File Offset: 0x0001678F
		internal static PrintingServices CreatePrintingService()
		{
			if (SysPrn.is_unix)
			{
				return new PrintingServicesUnix();
			}
			return new PrintingServicesWin32();
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000185A3 File Offset: 0x000167A3
		internal static GlobalPrintingServices GlobalService
		{
			get
			{
				if (SysPrn.global_printing_services == null)
				{
					if (SysPrn.is_unix)
					{
						SysPrn.global_printing_services = new GlobalPrintingServicesUnix();
					}
					else
					{
						SysPrn.global_printing_services = new GlobalPrintingServicesWin32();
					}
				}
				return SysPrn.global_printing_services;
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000185CE File Offset: 0x000167CE
		internal static void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			SysPrn.CreatePrintingService().GetPrintDialogInfo(printer, ref port, ref type, ref status, ref comment);
		}

		// Token: 0x04000736 RID: 1846
		private static GlobalPrintingServices global_printing_services;

		// Token: 0x04000737 RID: 1847
		private static bool is_unix = GDIPlus.RunningOnUnix();

		// Token: 0x020000D7 RID: 215
		internal class Printer
		{
			// Token: 0x06000B78 RID: 2936 RVA: 0x000185E0 File Offset: 0x000167E0
			public Printer(string port, string type, string status, string comment)
			{
				this.Port = port;
				this.Type = type;
				this.Status = status;
				this.Comment = comment;
			}

			// Token: 0x04000738 RID: 1848
			public readonly string Comment;

			// Token: 0x04000739 RID: 1849
			public readonly string Port;

			// Token: 0x0400073A RID: 1850
			public readonly string Type;

			// Token: 0x0400073B RID: 1851
			public readonly string Status;

			// Token: 0x0400073C RID: 1852
			public PrinterSettings Settings;
		}
	}
}
