using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the type of print operation occurring.</summary>
	// Token: 0x020000BB RID: 187
	public enum PrintAction
	{
		/// <summary>The print operation is printing to a file.</summary>
		// Token: 0x040006DF RID: 1759
		PrintToFile,
		/// <summary>The print operation is a print preview.</summary>
		// Token: 0x040006E0 RID: 1760
		PrintToPreview,
		/// <summary>The print operation is printing to a printer.</summary>
		// Token: 0x040006E1 RID: 1761
		PrintToPrinter
	}
}
