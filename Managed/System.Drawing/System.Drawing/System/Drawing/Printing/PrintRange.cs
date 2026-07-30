using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the part of the document to print.</summary>
	// Token: 0x020000BE RID: 190
	public enum PrintRange
	{
		/// <summary>All pages are printed.</summary>
		// Token: 0x040006E3 RID: 1763
		AllPages,
		/// <summary>The pages between <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> and <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> are printed.</summary>
		// Token: 0x040006E4 RID: 1764
		SomePages = 2,
		/// <summary>The selected pages are printed.</summary>
		// Token: 0x040006E5 RID: 1765
		Selection = 1,
		/// <summary>The currently displayed page is printed</summary>
		// Token: 0x040006E6 RID: 1766
		CurrentPage = 4194304
	}
}
