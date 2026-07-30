using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the printer's duplex setting.</summary>
	// Token: 0x020000B3 RID: 179
	public enum Duplex
	{
		/// <summary>The printer's default duplex setting.</summary>
		// Token: 0x04000643 RID: 1603
		Default = -1,
		/// <summary>Single-sided printing.</summary>
		// Token: 0x04000644 RID: 1604
		Simplex = 1,
		/// <summary>Double-sided, horizontal printing.</summary>
		// Token: 0x04000645 RID: 1605
		Horizontal = 3,
		/// <summary>Double-sided, vertical printing.</summary>
		// Token: 0x04000646 RID: 1606
		Vertical = 2
	}
}
