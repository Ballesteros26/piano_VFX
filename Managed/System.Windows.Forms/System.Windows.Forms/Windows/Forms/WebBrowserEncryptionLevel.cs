using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the encryption methods used by documents displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003AE RID: 942
	public enum WebBrowserEncryptionLevel
	{
		/// <summary>No security encryption.</summary>
		// Token: 0x04001CB8 RID: 7352
		Insecure,
		/// <summary>Multiple security encryption methods in different Web page frames.</summary>
		// Token: 0x04001CB9 RID: 7353
		Mixed,
		/// <summary>Unknown security encryption.</summary>
		// Token: 0x04001CBA RID: 7354
		Unknown,
		/// <summary>40-bit security encryption.</summary>
		// Token: 0x04001CBB RID: 7355
		Bit40,
		/// <summary>56-bit security encryption.</summary>
		// Token: 0x04001CBC RID: 7356
		Bit56,
		/// <summary>Fortezza security encryption.</summary>
		// Token: 0x04001CBD RID: 7357
		Fortezza,
		/// <summary>128-bit security encryption.</summary>
		// Token: 0x04001CBE RID: 7358
		Bit128
	}
}
