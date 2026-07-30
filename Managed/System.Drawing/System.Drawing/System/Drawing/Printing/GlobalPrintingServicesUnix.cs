using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000E2 RID: 226
	internal class GlobalPrintingServicesUnix : GlobalPrintingServices
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0001994E File Offset: 0x00017B4E
		internal override PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				return PrintingServicesUnix.InstalledPrinters;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00019955 File Offset: 0x00017B55
		internal override IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			return PrintingServicesUnix.CreateGraphicsContext(settings, default_page_settings);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001995E File Offset: 0x00017B5E
		internal override bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			return PrintingServicesUnix.StartDoc(gr, doc_name, output_file);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00019968 File Offset: 0x00017B68
		internal override bool EndDoc(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.EndDoc(gr);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00019970 File Offset: 0x00017B70
		internal override bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.StartPage(gr);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00019978 File Offset: 0x00017B78
		internal override bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.EndPage(gr);
		}
	}
}
