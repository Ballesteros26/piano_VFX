using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000EC RID: 236
	internal class GlobalPrintingServicesWin32 : GlobalPrintingServices
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0001A48D File Offset: 0x0001868D
		internal override PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				return PrintingServicesWin32.InstalledPrinters;
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001A494 File Offset: 0x00018694
		internal override IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			return PrintingServicesWin32.CreateGraphicsContext(settings, default_page_settings);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001A49D File Offset: 0x0001869D
		internal override bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			return PrintingServicesWin32.StartDoc(gr, doc_name, output_file);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001A4A7 File Offset: 0x000186A7
		internal override bool EndDoc(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.EndDoc(gr);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001A4AF File Offset: 0x000186AF
		internal override bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.StartPage(gr);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001A4B7 File Offset: 0x000186B7
		internal override bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.EndPage(gr);
		}
	}
}
