using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the type of printing that code is allowed to do.</summary>
	// Token: 0x020000C5 RID: 197
	[Serializable]
	public enum PrintingPermissionLevel
	{
		/// <summary>Prevents access to printers. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.NoPrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.SafePrinting" />.</summary>
		// Token: 0x040006F8 RID: 1784
		NoPrinting,
		/// <summary>Provides printing only from a restricted dialog box. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.SafePrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.DefaultPrinting" />.</summary>
		// Token: 0x040006F9 RID: 1785
		SafePrinting,
		/// <summary>Provides printing programmatically to the default printer, along with safe printing through semirestricted dialog box. <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.DefaultPrinting" /> is a subset of <see cref="F:System.Drawing.Printing.PrintingPermissionLevel.AllPrinting" />.</summary>
		// Token: 0x040006FA RID: 1786
		DefaultPrinting,
		/// <summary>Provides full access to all printers.</summary>
		// Token: 0x040006FB RID: 1787
		AllPrinting
	}
}
