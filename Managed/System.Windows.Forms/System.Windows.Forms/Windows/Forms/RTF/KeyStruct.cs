using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000026 RID: 38
	internal struct KeyStruct
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000061A4 File Offset: 0x000043A4
		public KeyStruct(Major major, Minor minor, string symbol)
		{
			this.Major = major;
			this.Minor = minor;
			this.Symbol = symbol;
		}

		// Token: 0x0400007B RID: 123
		public Major Major;

		// Token: 0x0400007C RID: 124
		public Minor Minor;

		// Token: 0x0400007D RID: 125
		public string Symbol;
	}
}
