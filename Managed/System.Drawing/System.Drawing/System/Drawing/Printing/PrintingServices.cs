using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D4 RID: 212
	internal abstract class PrintingServices
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000B65 RID: 2917
		internal abstract string DefaultPrinter { get; }

		// Token: 0x06000B66 RID: 2918
		internal abstract bool IsPrinterValid(string printer);

		// Token: 0x06000B67 RID: 2919
		internal abstract void LoadPrinterSettings(string printer, PrinterSettings settings);

		// Token: 0x06000B68 RID: 2920
		internal abstract void LoadPrinterResolutions(string printer, PrinterSettings settings);

		// Token: 0x06000B69 RID: 2921
		internal abstract void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment);

		// Token: 0x06000B6A RID: 2922 RVA: 0x00018534 File Offset: 0x00016734
		internal void LoadDefaultResolutions(PrinterSettings.PrinterResolutionCollection col)
		{
			col.Add(new PrinterResolution(PrinterResolutionKind.High, -4, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Medium, -3, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Low, -2, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Draft, -1, -1));
		}
	}
}
