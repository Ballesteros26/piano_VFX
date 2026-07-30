using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D5 RID: 213
	internal abstract class GlobalPrintingServices
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000B6C RID: 2924
		internal abstract PrinterSettings.StringCollection InstalledPrinters { get; }

		// Token: 0x06000B6D RID: 2925
		internal abstract IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings page_settings);

		// Token: 0x06000B6E RID: 2926
		internal abstract bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file);

		// Token: 0x06000B6F RID: 2927
		internal abstract bool StartPage(GraphicsPrinter gr);

		// Token: 0x06000B70 RID: 2928
		internal abstract bool EndPage(GraphicsPrinter gr);

		// Token: 0x06000B71 RID: 2929
		internal abstract bool EndDoc(GraphicsPrinter gr);
	}
}
